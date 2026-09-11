namespace Yandex.Messenger.Bot.Tests;

using System.Reflection;
using AspNetCore.Extensions;
using AspNetCore.Options;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sdk;
using Sdk.Abstractions;

public class DependencyInjectionTests
{
    [Fact]
    public void AddYandexMessengerBotSdk_RegistersAllPublicServices()
    {
        using var provider = BuildServiceProvider("test-token");

        provider.GetRequiredService<IYandexMessengerBotClient>().Should().NotBeNull();
        provider.GetRequiredService<IChats>().Should().NotBeNull();
        provider.GetRequiredService<IPolls>().Should().NotBeNull();
        provider.GetRequiredService<IUpdates>().Should().NotBeNull();
        provider.GetRequiredService<IUpdateProcessor>().Should().NotBeNull();
    }

    [Fact]
    public void AddYandexMessengerBotSdk_ConfiguresHttpClientWithBaseAddressAndOAuthToken()
    {
        using var provider = BuildServiceProvider("test-token");

        var client = provider.GetRequiredService<IYandexMessengerBotClient>();
        var httpClient = (HttpClient)typeof(YandexMessengerBotClient)
            .GetField("_httpClient", BindingFlags.Instance | BindingFlags.NonPublic)!
            .GetValue(client)!;

        httpClient.BaseAddress
            .Should().Be(new Uri(YandexMessengerBotClient.YandexMessengerBotApiBaseAddress));
        httpClient.DefaultRequestHeaders.Authorization.Should().NotBeNull();
        httpClient.DefaultRequestHeaders.Authorization!.Scheme.Should().Be("OAuth");
        httpClient.DefaultRequestHeaders.Authorization!.Parameter.Should().Be("test-token");
    }

    [Fact]
    public void AddYandexMessengerBotSdk_WithEmptyToken_ThrowsOnOptionsAccess()
    {
        using var provider = BuildServiceProvider(token: string.Empty);

        var options = provider.GetRequiredService<IOptions<YandexMessengerBotOptions>>();

        var act = () => options.Value;

        act.Should().Throw<OptionsValidationException>()
            .Which.Failures.Should().Contain(f => f.Contains("Token is required"));
    }

    private static ServiceProvider BuildServiceProvider(string token)
    {
        var services = new ServiceCollection();
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                [$"{YandexMessengerBotOptions.SectionName}:Token"] = token,
                [$"{YandexMessengerBotOptions.SectionName}:WebhookEndpoint"] = "/hook",
            })
            .Build();

        services.AddYandexMessengerBotSdk(config);
        return services.BuildServiceProvider();
    }
}
