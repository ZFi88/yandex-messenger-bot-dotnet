namespace Yandex.Messenger.Bot.AspNetCore.Extensions;

using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Middleware;
using Options;
using Sdk;
using Sdk.Abstractions;
using Sdk.Json;
using Sdk.Models;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddYandexMessengerBotSdk(this IServiceCollection services, IConfiguration cfg)
    {
        services.Configure<YandexMessengerBotOptions>(cfg.GetSection(YandexMessengerBotOptions.SectionName));
        services.AddHttpClient<IYandexMessangerBotClient>((provider, client) =>
        {
            var options = provider.GetService<IOptions<YandexMessengerBotOptions>>();
            client.BaseAddress = new Uri(YandexMessengerBotClient.YandexMessengerBotApiBaseAddress);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("OAuth", options!.Value.Token);
        });
        services.AddTransient<IYandexMessangerBotClient, YandexMessengerBotClient>(provider =>
            {
                var httpClient = provider.GetService<IHttpClientFactory>()!
                    .CreateClient(nameof(IYandexMessangerBotClient));
                return new YandexMessengerBotClient(httpClient);
            })
            .AddTransient<IChats>(provider => provider.GetRequiredService<IYandexMessangerBotClient>().Chats)
            .AddTransient<IPolls>(provider => provider.GetRequiredService<IYandexMessangerBotClient>().Polls)
            .AddTransient<IUpdates>(provider => provider.GetRequiredService<IYandexMessangerBotClient>().Updates);

        return services;
    }

    public static IServiceCollection AddYandexMessengerObserver(
        this IServiceCollection services,
        string message,
        Func<IServiceProvider, Update, Task> messageHandler)
    {
        return services.AddTransient<IObserver>(provider => new WebhookObserver(provider, message, messageHandler));
    }

    public static IServiceCollection AddYandexMessengerObserver(
        this IServiceCollection services,
        Func<IServiceProvider, Update, Task> messageHandler)
    {
        return services.AddTransient<IObserver>(provider => new WebhookObserver(provider, string.Empty, messageHandler));
    }

    public static IServiceCollection AddYandexMessengerObserver<TObserver>(this IServiceCollection services)
        where TObserver : class, IObserver
    {
        return services.AddTransient<IObserver, TObserver>();
    }
}