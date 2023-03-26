namespace Yandex.Messenger.Bot.AspNetCore.Extensions;

using Microsoft.AspNetCore.Builder;
using Middleware;

/// <summary>
/// Extensions for an application builder.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds middleware for handling webhooks from Yandex Messenger Bot API to the ASPNET core request pipeline.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    public static IApplicationBuilder UseYandexMessengerWebhook(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<WebhookMiddleware>();
    }
}