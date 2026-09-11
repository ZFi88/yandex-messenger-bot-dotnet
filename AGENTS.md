# AGENTS.md

This file provides guidance to AI agents when working with code in this repository.

## Overview

.NET wrapper for the [Yandex Messenger Bot API](https://botapi.messenger.yandex.net/docs/). Two NuGet packages:

- **Yandex.Messenger.Bot.Sdk** — HTTP client, models, and update observer abstractions covering the Bot API
- **Yandex.Messenger.Bot.AspNetCore** — DI registration and webhook middleware for ASP.NET Core

SDK target: `netstandard2.0` (C# 13). Tests and examples: `net9.0`. SDK version is pinned in `global.json` (`9.0.100`).

## Commands

Build orchestration is **Nuke** (default configuration: `Release`). Prefer the wrapper scripts over invoking Nuke directly.

```bash
./build.sh              # default target: Compile (Clean → Restore → Compile)
./build.sh Clean
./build.sh Restore
./build.sh Compile
./build.sh Test          # depends on Compile
./build.sh Pack          # depends on Test; writes nupkgs to artifacts/
./build.sh Publish       # packs then pushes only if git tags matching packable projects point at HEAD; needs NUGET_API_KEY
./build.sh Tag           # creates tags {ProjectName}.{Version} for packable projects

# Windows equivalents: build.cmd / build.ps1
```

Override configuration: `./build.sh Compile --configuration Debug`

Day-to-day `dotnet` workflows (without full Nuke Clean):

```bash
dotnet restore yandex-messenger-bot-dotnet.sln
dotnet build yandex-messenger-bot-dotnet.sln -c Debug
dotnet test tests/Yandex.Messenger.Bot.Tests/Yandex.Messenger.Bot.Tests.csproj -c Debug

# Single test class / method (xUnit filter)
dotnet test tests/Yandex.Messenger.Bot.Tests/Yandex.Messenger.Bot.Tests.csproj --filter "FullyQualifiedName~UpdateProcessorTests"
dotnet test tests/Yandex.Messenger.Bot.Tests/Yandex.Messenger.Bot.Tests.csproj --filter "FullyQualifiedName~UpdateProcessorTests.Process_When"

# Examples
dotnet run --project examples/Example.Console
dotnet run --project examples/Example.WebApp
```

There is no separate lint target. StyleCop.Analyzers run on build for `src/` via `stylecop.ruleset`; XML docs are generated; Release treats warnings as errors (`TreatWarningsAsErrors`).

CI (`.github/workflows/CI.yml`, generated from Nuke `[GitHubActions]`): on push to `develop`, `master`, `release/*`, `bugfix/*` runs `./build.cmd Publish` on `windows-latest` with `NUGET_API_KEY`. Publish is a no-op unless version tags point at the current commit.

Package versions are centralized: `src/Directory.Packages.props`, `tests/Directory.Packages.props`. Package metadata / version (`1.0.2` currently) live in `src/Directory.Build.props`.

## Architecture

### Solution layout

| Path | Role |
|------|------|
| `src/Yandex.Messenger.Bot.Sdk` | Packable core library |
| `src/Yandex.Messenger.Bot.AspNetCore` | Packable ASP.NET Core integration (references Sdk) |
| `tests/Yandex.Messenger.Bot.Tests` | xunit + Moq + MockHttp + FluentAssertions + AutoFixture |
| `examples/Example.Console` | Long-poll echo bot |
| `examples/Example.WebApp` | Webhook + DI observers |
| `build/` | Nuke build project (`_build.csproj`) |

Internals of the Sdk are visible to AspNetCore, Tests, and Moq (`InternalsVisibleTo` in the Sdk csproj).

### SDK request pipeline

```
YandexMessengerBotClient
  ├── Chats  : IChats   → BaseClient.Send + ISendStrategy
  ├── Polls  : IPolls
  └── Updates: IUpdates → GetUpdates/SetWebhook + observer subscription
```

- Base API URL: `https://botapi.messenger.yandex.net/bot/v1/` (`YandexMessengerBotClient.YandexMessengerBotApiBaseAddress`).
- Auth: `Authorization: OAuth {token}` on the shared `HttpClient`.
- `BaseClient.Send<TResp>` builds the request via an `ISendStrategy`, requires HTTP 200 and `Response.Ok == true`, otherwise throws `BotException`.
- Strategies under `Impl/Strategies/`:
  - `SendJsonStrategy` — POST JSON body (most endpoints)
  - `SendJsonToQueryStringStrategy` — GET with query string (e.g. `users/getUserLink`)
  - `SendFileStrategy` / `SendImageStrategy` / `SendAlbumStrategy` — multipart (`MultipartStrategy`)
- JSON: `YandexMessengerBotJsonOptions` — snake_case naming (`UnderscorePolicy`), case-insensitive, ignore default when writing, `TimestampToDateTimeConverter`.
- `RecordHelpers.cs` polyfills `IsExternalInit` / required-members attributes so records/`init` work on netstandard2.0 — do not remove casually.

Public API surface is interfaces under `Abstractions/` (`IYandexMessengerBotClient`, `IChats`, `IPolls`, `IUpdates`, `IObserver`, `IObservable`, `IUpdateProcessor`). Concrete clients in `Impl/` are internal. DTOs live in `Models/` (`Requests/`, `Responses/`, domain types like `Update`, `Chat`, `User`).

Endpoint → method mapping is the source of truth for API coverage; `EndpointTests` asserts each SDK call hits the expected relative path/method via MockHttp.

### Update handling (observer model)

`UpdateProcessor` routes each `Update` to registered `IObserver`s keyed by `IObserver.Message`:

1. Observers with `Message == null` / empty string → all updates (global)
2. If `update.CallbackData != null` → observers keyed by button GUID string
3. If `update.Text` is non-empty → observers keyed by exact text match (e.g. `"/help"`)

Two consumption modes:

1. **Polling (console)**: `Updates.GetUpdates` keeps an internal offset, fetches `messages/getUpdates`, then runs the client's private `UpdateProcessor`. Subscribe via `IUpdates.Subscribe(IObserver)` or helpers in `Extensions/UpdatesExtensions` (all / by text / by button `Guid`).
2. **Webhooks (ASP.NET)**: separate DI-scoped path — see below. Does not use the client's internal processor.

### AspNetCore integration

Config section `YandexMessengerBot` (`YandexMessengerBotOptions`): `Token`, `WebhookEndpoint` (default `/hook`).

- `AddYandexMessengerBotSdk(IConfiguration)` — options, typed `HttpClient`, `IYandexMessengerBotClient` / `IChats` / `IPolls` / `IUpdates`, and `IUpdateProcessor` → `UpdateProcessor` (DI injects all registered `IObserver`s into the ctor).
- Observer registration:
  - `AddYandexMessengerObserver(handler)` — global
  - `AddYandexMessengerObserver(message, handler)` — text match
  - `AddYandexMessengerObserver<TObserver>()` — custom `IObserver`
  - `AddYandexButtonObserver(buttonId, handler)` — button GUID
  Handlers receive `IServiceProvider` so they can resolve `IChats` etc. (`WebhookObserver`).
- `UseYandexMessengerWebhook()` — `WebhookMiddleware` matches the configured path, deserializes body as either `GetUpdateResponse` (batch) or single `Update`, dispatches through `IUpdateProcessor`, returns 200. Failures are logged; middleware still responds OK for matched paths.

### Tests

Unit-level only (no live API): MockHttp for endpoint contracts; processor/subscription behavior; JSON naming and timestamp conversion. When adding API methods: extend the corresponding `IChats`/`IPolls`/`IUpdates` impl + strategy, request/response models, and a row in `EndpointTests.Data()`.

### Conventions specific to this repo

- XML documentation on public API; StyleCop enforced on `src/` (many SA rules as Error; SA1101 this-qualifier off; SA1309 underscore fields allowed).
- Prefer matching existing patterns: thin public interfaces, internal `BaseClient` + strategy, snake_case JSON via shared options, observer keying by message text or button id string.

### Git commit messages

Use [Conventional Commits](https://www.conventionalcommits.org/):

```
<type>[optional scope]: <description>

[optional body]

[optional footer(s)]
```

- **type** (required): `feat`, `fix`, `docs`, `style`, `refactor`, `perf`, `test`, `build`, `ci`, `chore`, `revert`
- **scope** (optional): area of change, e.g. `webhook-middleware`, `sdk`, `aspnetcore`, `build`
- **description**: short summary after the colon and space; do not end with a period
- Breaking changes: `!` after type/scope (e.g. `feat(sdk)!: ...`) and/or a `BREAKING CHANGE:` footer

Examples from this repo: `fix(webhook-middleware): Added serialization fallback`, `build: Added Test target`, `chore: Updated packages`.
