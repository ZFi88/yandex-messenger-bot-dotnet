namespace Yandex.Messenger.Bot.AspNetCore.Extensions;

using Microsoft.AspNetCore.Builder;
using Middleware;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseYandexMessangerWebhook(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<WebhookMiddleware>();
    }
}