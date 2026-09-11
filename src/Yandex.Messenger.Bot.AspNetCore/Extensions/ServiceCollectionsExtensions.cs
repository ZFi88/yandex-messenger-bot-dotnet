namespace Yandex.Messenger.Bot.AspNetCore.Extensions;

using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Middleware;
using Options;
using Sdk;
using Sdk.Abstractions;
using Sdk.Impl;
using Sdk.Models;

/// <summary>
/// Extensions for service collection builder.
/// </summary>
public static class ServiceCollectionsExtensions
{
    /// <summary>
    /// The logical name of the named HTTP client used by the Yandex Messenger Bot client.
    /// The same name is used both when registering the client with the HTTP client factory and
    /// when resolving it, so the configuration (base address and OAuth header) is always applied.
    /// </summary>
    private const string HttpClientName = "YandexMessengerBot";

    /// <summary>
    /// Adds and configures the Yandex Messenger Bot SDK services into the DI container.
    /// </summary>
    /// <param name="services">The DI container.</param>
    /// <param name="cfg">The application configuration.</param>
    public static IServiceCollection AddYandexMessengerBotSdk(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddOptions<YandexMessengerBotOptions>()
            .Bind(cfg.GetSection(YandexMessengerBotOptions.SectionName))
            .Validate(o => !string.IsNullOrWhiteSpace(o.Token), "Token is required");

        services.AddHttpClient(HttpClientName, (provider, client) =>
        {
            var options = provider.GetRequiredService<IOptions<YandexMessengerBotOptions>>().Value;
            client.BaseAddress = new Uri(YandexMessengerBotClient.YandexMessengerBotApiBaseAddress);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("OAuth", options.Token);
        });

        services.AddTransient<IYandexMessengerBotClient, YandexMessengerBotClient>(provider =>
            {
                var httpClient = provider.GetRequiredService<IHttpClientFactory>()
                    .CreateClient(HttpClientName);
                return new YandexMessengerBotClient(httpClient);
            })
            .AddTransient<IChats>(provider => provider.GetRequiredService<IYandexMessengerBotClient>().Chats)
            .AddTransient<IPolls>(provider => provider.GetRequiredService<IYandexMessengerBotClient>().Polls)
            .AddTransient<IUpdates>(provider => provider.GetRequiredService<IYandexMessengerBotClient>().Updates)
            .AddTransient<IUpdateProcessor, UpdateProcessor>();

        return services;
    }

    /// <summary>
    /// Adds an observer into the DI container.
    /// </summary>
    /// <param name="services">The DI container.</param>
    /// <param name="message">The text of a message for observing.</param>
    /// <param name="messageHandler">A function for updates handling.</param>
    public static IServiceCollection AddYandexMessengerObserver(
        this IServiceCollection services,
        string message,
        Func<IServiceProvider, Update, CancellationToken, Task> messageHandler)
    {
        return services.AddTransient<IObserver>(provider => new WebhookObserver(provider, message, messageHandler));
    }

    /// <summary>
    /// Adds an observer into the DI container.
    /// </summary>
    /// <param name="services">The DI container.</param>
    /// <typeparam name="TObserver">The type of observer.</typeparam>
    public static IServiceCollection AddYandexMessengerObserver<TObserver>(
        this IServiceCollection services)
        where TObserver : class, IObserver
    {
        return services.AddTransient<IObserver, TObserver>();
    }

    /// <summary>
    /// Adds common observer which handles all updates from the Yandex Messenger Bot API.
    /// </summary>
    /// <param name="services">The DI container.</param>
    /// <param name="messageHandler">An update handler.</param>
    public static IServiceCollection AddYandexMessengerObserver(
        this IServiceCollection services,
        Func<IServiceProvider, Update, CancellationToken, Task> messageHandler)
    {
        return services.AddTransient<IObserver>(provider =>
            new WebhookObserver(provider, string.Empty, messageHandler));
    }

    /// <summary>
    /// Adds a button observer into the DI container.
    /// </summary>
    /// <param name="services">The DI container.</param>
    /// <param name="buttonId">An ID of a button for observing.</param>
    /// <param name="messageHandler">A function for updates handling.</param>
    public static IServiceCollection AddYandexButtonObserver(
        this IServiceCollection services,
        Guid buttonId,
        Func<IServiceProvider, Update, CancellationToken, Task> messageHandler)
    {
        return services.AddTransient<IObserver>(provider =>
            new WebhookObserver(provider,
                buttonId.ToString(),
                messageHandler));
    }
}