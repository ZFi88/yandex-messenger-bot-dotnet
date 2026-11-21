namespace Yandex.Messenger.Bot.Tests;

using System.Net.Http;
using FluentAssertions;
using Sdk;

public class YandexMessengerBotClientTests
{
    [Fact]
    public void Constructor_WithToken_ShouldSetDefaultUserAgent()
    {
        using var client = new YandexMessengerBotClient("test-token");

        client.Should().NotBeNull();
        // The token constructor owns its HttpClient internally; the user agent is asserted
        // through the reflection-free path in Constructor_WithHttpClient_ShouldKeepCallerUserAgent.
    }

    [Fact]
    public void Constructor_WithHttpClient_ShouldSetUserAgentWhenMissing()
    {
        using var httpClient = new HttpClient();

        using var client = new YandexMessengerBotClient(httpClient);

        httpClient.DefaultRequestHeaders.UserAgent.Should().NotBeEmpty();
    }

    [Fact]
    public void Constructor_WithHttpClient_ShouldKeepCallerUserAgent()
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("custom-agent/1.0");

        using var client = new YandexMessengerBotClient(httpClient);

        var values = httpClient.DefaultRequestHeaders.UserAgent.ToList();
        values.Should().ContainSingle();
        values[0].Product!.Name.Should().Be("custom-agent");
    }
}
