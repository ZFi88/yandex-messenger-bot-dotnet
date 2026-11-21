# Yandex Messenger Bot .NET SDK
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/zfi88/yandex-messenger-bot-dotnet/CI.yml?label=CI)

![forks](https://img.shields.io/github/forks/zfi88/yandex-messenger-bot-dotnet.svg)
![stars](https://img.shields.io/github/stars/zfi88/yandex-messenger-bot-dotnet.svg)

![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Yandex.Messenger.Bot.Sdk?label=Yandex.Messenger.Bot.Sdk)

![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Yandex.Messenger.Bot.AspNetCore?label=Yandex.Messenger.AspNetCore)

This repository represents dotnet wrapper for Yandex Messenger Bot API.
The repository contains two libraries:
* [Yandex.Messenger.Bot.Sdk](https://www.nuget.org/packages/Yandex.Messenger.Bot.Sdk) -
contains objects, requests and abstractions that cover the Bot API endpoints (see [API coverage](#api-coverage)).
* [Yandex.Messenger.Bot.AspNetCore](https://www.nuget.org/packages/Yandex.Messenger.Bot.AspNetCore) - 
contains numbers of extension methods for integrating SDK into an ASP.NET Core applications using webhooks.

--------------------

## Usage

### Pre requirements

Create a new bot and generate a new access token in the [admin console](https://admin.yandex.ru/bot-platform). Also for
ASP.NET applications you can setup a webhook url.

### Console applications

Install package: `dotnet add package Yandex.Messenger.Bot.Sdk`

Create the bot client using generated access token:
```csharp
var yandexBotClient = new YandexMessengerBotClient("<TOKEN>");
```
Subscribe on all messages:
```csharp
yandexBotClient.Updates.Subscribe(async (update, cancel) =>
{
    await yandexBotClient.Chats.SendMessage(new SendMessageRequest
    {
        Text = $"{update.From.Login} you sent: \"{update.Text}\"",
        ChatId = update.Chat.Id
    }, cancel);
});
```

Or subscribe on concrete messages:
```csharp
yandexBotClient.Updates.Subscribe("/help", async (update, token) =>
{
    await yandexBotClient.Chats.SendMessage(new SendMessageRequest()
    {
        Text = $"Hi, {update.From.Login}! I'm EchoBot, I'll repeat all your messages!", 
        ChatId = update.Chat.Id
    }, token);
})
```

Or subscribe on button click:
```csharp
var guid = Guid.Parse("B27943C9-DED4-4E31-A381-DB23A7CF0813");
yandexBotClient.Updates.Subscribe(guid, async (update, token) =>
{
    await yandexBotClient.Chats.SendMessage(new SendMessageRequest()
    {
        Text = $"Hi, {update.From.Login}! I'm EchoBot, I clicked button with id - {update.CallbackData!.Id}!", 
        ChatId = update.Chat.Id
    }, token);
})
```

Poll updates from chats:
```csharp
while (true)
{
    await yandexBotClient.Updates.GetUpdates(new GetUpdateRequest());
    await Task.Delay(3000);
}
```

### ASP.NET Core applications

Install package: `dotnet add package Yandex.Messenger.Bot.AspNetCore`

Add configuration to your application:
```json
{
  "YandexMessengerBot": {
    "Token": "<TOKEN>",
    "WebhookEndpoint": "/hook"
  }
}
```

Setup a container:
```csharp
builder.Services.AddYandexMessengerBotSdk(builder.Configuration);
```

Subscribe on messages:
```csharp
builder.Services.AddYandexMessengerObserver(async (provider, update, cancellationToken) =>
{
    var logger = provider.GetService<ILogger<Program>>();
    logger!.LogInformation(
        "User {login} sent message: {msg}.",
        update.From.Login,
        update.Text);

    var chats = provider.GetService<IChats>();
    await chats!.SendMessage(new SendMessageRequest
    {
        Text = $"{update.From.Login} you sent: \"{update.Text}\"",
        ChatId = update.Chat.Id
    }, cancellationToken);
});
```

Insert webhook handler into a middleware pipeline:
```csharp
app.UseYandexMessengerWebhook();
```

Look for the full [examples](https://github.com/ZFi88/yandex-messenger-bot-dotnet/tree/develop/examples)!

## API coverage

The SDK covers every endpoint of the [Yandex Messenger Bot API](https://botapi.messenger.yandex.net/docs/):

| Area | Interface | Methods |
|------|-----------|---------|
| Bot | `IBot` | `GetSelf`, `UpdateSelf` |
| Chats | `IChats` | `Create`, `UpdateChat`, `GetChats`, `GetChat`, `GetMembers`, `GetUserLink` |
| Messages | `IMessages` | `SendSticker`, `SendSystemMessage`, `SendTyping`, `Pin`, `Unpin`, `SendReaction`, `GetReactions`, `ShareFile`, `ShareImage`, `ShareGallery`, `GetFile` |
| Polls | `IPolls` | `CreatePoll`, `GetPollResults`, `GetVoters` |
| Updates | `IUpdates` | `GetUpdates`, `SetWebhook`, `Subscribe` |

\* Text messages, files, images and albums are sent through `IChats` for backward compatibility;
`IMessages` contains the rest of the message methods.

Notes:

- `IMessages.GetFile` returns a `Stream` that owns the HTTP response. Dispose it after reading the file.
- Suggest buttons and action buttons are configured with `SuggestButtons` and `ActionButtons`
  on `SendMessageRequest` and the other send requests (`SendFileRequest`, `SendAlbumRequest`, `SendStickerRequest`, ...).
- Passing `WebhookUrl = null` to `SetWebhookRequest` resets the webhook.
- Bot event flags (`get_reactions`, `get_members_changed`) are configured through `IBot.UpdateSelf`.
