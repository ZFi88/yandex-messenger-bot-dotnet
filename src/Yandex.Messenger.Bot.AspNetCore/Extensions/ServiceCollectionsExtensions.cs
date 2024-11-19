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
    /// Adds the Yandex Messenger Bot SDK to a DI container of an application.
    /// </summary>
    /// <param name="services">The DI container.</param>
    /// <param name="cfg">The application configuration.</param>
    public static IServiceCollection AddYandexMessengerBotSdk(this IServiceCollection services, IConfiguration cfg)
    {
        services.Configure<YandexMessengerBotOptions>(cfg.GetSection(YandexMessengerBotOptions.SectionName));
        services.AddHttpClient<IYandexMessengerBotClient>((provider, client) =>
        {
            var options = provider.GetService<IOptions<YandexMessengerBotOptions>>();
            client.BaseAddress = new Uri(YandexMessengerBotClient.YandexMessengerBotApiBaseAddress);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("OAuth", options!.Value.Token);
        });
        services.AddTransient<IYandexMessengerBotClient, YandexMessengerBotClient>(provider =>
            {
                var httpClient = provider.GetService<IHttpClientFactory>()!
                    .CreateClient(nameof(IYandexMessengerBotClient));
                return new YandexMessengerBotClient(httpClient);
            })
            .AddTransient<IChats>(provider => provider.GetRequiredService<IYandexMessengerBotClient>().Chats)
            .AddTransient<IPolls>(provider => provider.GetRequiredService<IYandexMessengerBotClient>().Polls)
            .AddTransient<IUpdates>(provider => provider.GetRequiredService<IYandexMessengerBotClient>().Updates)
            .AddTransient<IUpdateProcessor, UpdateProcessor>();

        return services;
    }

    /// <summary>
    /// Adds an observer of an updates.
    /// </summary>
    /// <param name="services">The DIN container.</param>
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
    /// Adds an observer of an updates.
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

    /// <summary>
    /// Adds common observer which handles all updates from the Yandex Messenger Bot API.
    /// </summary>
    /// <param name="services">The DI container.</param>
    /// <param name="messageHandler">A function for updates handling.</param>
    public static IServiceCollection AddYandexMessengerObserver(
        this IServiceCollection services,
        Func<IServiceProvider, Update, CancellationToken, Task> messageHandler)
    {
        return services.AddTransient<IObserver>(provider =>
            new WebhookObserver(provider, string.Empty, messageHandler));
    }

    /// <summary>
    /// Adds an observer of an updates.
    /// </summary>
    /// <param name="services">The DI container.</param>
    /// <typeparam name="TObserver">The type of observer.</typeparam>
    public static IServiceCollection AddYandexMessengerObserver<TObserver>(this IServiceCollection services)
        where TObserver : class, IObserver
    {
        return services.AddTransient<IObserver, TObserver>();
    }
}