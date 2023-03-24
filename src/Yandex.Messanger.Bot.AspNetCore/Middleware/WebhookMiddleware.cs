namespace Yandex.Messanger.Bot.AspNetCore.Middleware;

using System.Net;
using System.Text.Json;
using System.Xml;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Options;
using Sdk.Abstractions;
using Sdk.Exceptions;
using Sdk.Json;
using Sdk.Models;

public class WebhookMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IOptions<WebhookOptions> _options;
    private WebhookOptions _webhookOptions;

    public WebhookMiddleware(RequestDelegate next, IOptions<WebhookOptions> options)
    {
        _next = next;
        _options = options;
        if (_options.Value != null && _options.Value.WebhookUrl != null)
        {
            throw new BotException();
        }

        _webhookOptions = _options.Value!;
    }

    public async Task InvokeAsync(HttpContext context, IYandexBotClient client)
    {
        var webhookUrl = _webhookOptions.WebhookUrl!;
        if (webhookUrl.PathAndQuery == context.Request.Path.Value)
        {
            var observers = client.Updates.Observers;
            var updates = JsonSerializer.Deserialize<Update[]>(context.Request.Body, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = new SerializePolicy()
            });
            foreach (var update in updates)
            {
                if (observers.TryGetValue(string.Empty, out var observer))
                {
                    await observer.OnNewUpdate(update);
                }

                if (observers.TryGetValue(update.Text, out observer))
                {
                    await observer.OnNewUpdate(update);
                }
            }

            context.Response.StatusCode = (int)HttpStatusCode.OK;
        }

        await _next(context);
    }
}