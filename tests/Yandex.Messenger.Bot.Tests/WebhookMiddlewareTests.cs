namespace Yandex.Messenger.Bot.Tests;

using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using AspNetCore.Extensions;
using AspNetCore.Options;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Abstractions;
using Sdk.Json;
using Sdk.Models;
using Sdk.Models.Responses;

public class WebhookMiddlewareTests
{
    private const string HookPath = "/hook";

    [Fact]
    public async Task ProcessData_BatchPayload_ProcessesAllMatchingObserversOnce()
    {
        var globalObserver = new Mock<IObserver>();
        globalObserver.SetupGet(x => x.Message).Returns(string.Empty);

        var helloObserver = new Mock<IObserver>();
        helloObserver.SetupGet(x => x.Message).Returns("hello");

        var (server, _) = CreateServer(services =>
        {
            services.AddTransient<IObserver>(_ => globalObserver.Object);
            services.AddTransient<IObserver>(_ => helloObserver.Object);
        });

        using var client = server.CreateClient();
        var batch = JsonSerializer.Serialize(
            new GetUpdateResponse(true, "ok", new[]
            {
                MakeUpdate("hello", 1),
                MakeUpdate("world", 2),
            }),
            YandexMessengerBotJsonOptions.Value);

        var response = await client.PostAsync(HookPath, JsonContent(batch));

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        globalObserver
            .Verify(x => x.OnNewUpdate(It.IsAny<Update>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        helloObserver
            .Verify(x => x.OnNewUpdate(It.Is<Update>(u => u.Text == "hello"), It.IsAny<CancellationToken>()), Times.Once);
        helloObserver
            .Verify(x => x.OnNewUpdate(It.Is<Update>(u => u.Text == "world"), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ProcessData_SingleUpdate_ProcessedExactlyOnceWithoutNullReference()
    {
        var globalObserver = new Mock<IObserver>();
        globalObserver.SetupGet(x => x.Message).Returns(string.Empty);

        var (server, loggerProvider) = CreateServer(services =>
        {
            services.AddTransient<IObserver>(_ => globalObserver.Object);
        });

        using var client = server.CreateClient();
        var single = JsonSerializer.Serialize(MakeUpdate("hello", 1), YandexMessengerBotJsonOptions.Value);

        var response = await client.PostAsync(HookPath, JsonContent(single));

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        globalObserver
            .Verify(x => x.OnNewUpdate(It.IsAny<Update>(), It.IsAny<CancellationToken>()), Times.Once);
        loggerProvider.Entries
            .Should().NotContain(e => e.Level == LogLevel.Error || e.Level == LogLevel.Critical);
    }

    [Fact]
    public async Task ProcessData_InvalidBody_LogsAndReturnsOk()
    {
        var (server, loggerProvider) = CreateServer();

        using var client = server.CreateClient();
        var response = await client.PostAsync(HookPath, JsonContent("not a valid json {{{"));

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        loggerProvider.Entries
            .Should().Contain(e => e.Level == LogLevel.Error || e.Level == LogLevel.Critical);
    }

    [Fact]
    public async Task InvokeAsync_EndpointMismatch_CallsNext()
    {
        var (server, _) = CreateServer();

        using var client = server.CreateClient();
        var response = await client.PostAsync("/other-path", JsonContent("{}"));

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        (await response.Content.ReadAsStringAsync()).Should().Be("next");
    }

    private static (TestServer Server, CapturingLoggerProvider LoggerProvider) CreateServer(
        Action<IServiceCollection>? configureServices = null)
    {
        var loggerProvider = new CapturingLoggerProvider();
        var builder = new WebHostBuilder()
            .UseTestServer()
            .ConfigureLogging(logging => logging.AddProvider(loggerProvider))
            .ConfigureServices(services =>
            {
                services.AddYandexMessengerBotSdk(CreateConfiguration());
                configureServices?.Invoke(services);
            })
            .Configure(app =>
            {
                app.UseYandexMessengerWebhook();
                app.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync("next");
                });
            });

        var server = new TestServer(builder);
        return (server, loggerProvider);
    }

    private static IConfiguration CreateConfiguration() =>
        new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                [$"{YandexMessengerBotOptions.SectionName}:Token"] = "test-token",
                [$"{YandexMessengerBotOptions.SectionName}:WebhookEndpoint"] = HookPath,
            })
            .Build();

    private static Update MakeUpdate(string text, long updateId) =>
        new(new User("alice"), new Chat("1", ChatType.Private), text, DateTime.UnixEpoch, 10, updateId, null!, null);

    private static StringContent JsonContent(string json) =>
        new(json, Encoding.UTF8, "application/json");

    private sealed class CapturingLoggerProvider : ILoggerProvider
    {
        public ConcurrentBag<LogEntry> Entries { get; } = new();

        public ILogger CreateLogger(string categoryName) => new CapturingLogger(categoryName, Entries);

        public void Dispose()
        {
        }

        private sealed class CapturingLogger : ILogger
        {
            private readonly string _categoryName;
            private readonly ConcurrentBag<LogEntry> _entries;

            public CapturingLogger(string categoryName, ConcurrentBag<LogEntry> entries)
            {
                _categoryName = categoryName;
                _entries = entries;
            }

            public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

            public bool IsEnabled(LogLevel logLevel) => true;

            public void Log<TState>(
                LogLevel logLevel,
                EventId eventId,
                TState state,
                Exception? exception,
                Func<TState, Exception?, string> formatter)
            {
                _entries.Add(new LogEntry(_categoryName, logLevel, exception));
            }
        }
    }

    private sealed record LogEntry(string CategoryName, LogLevel Level, Exception? Exception);
}
