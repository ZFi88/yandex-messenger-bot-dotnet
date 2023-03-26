namespace Yandex.Messenger.Bot.AspNetCore.Middleware;

using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Options;
using Sdk.Abstractions;
using Sdk.Exceptions;
using Sdk.Json;
using Sdk.Models.Responses;

public class WebhookMiddleware
{
    private readonly RequestDelegate _next;
    private readonly YandexMessengerBotOptions _yandexMessengerBotOptions;

    public WebhookMiddleware(RequestDelegate next, IOptions<YandexMessengerBotOptions> options)
    {
        _next = next;
        if (options.Value != null && options.Value.WebhookUrl == null)
        {
            throw new BotException();
        }

        _yandexMessengerBotOptions = options.Value!;
    }

    public async Task InvokeAsync(HttpContext context, IEnumerable<IObserver> observers)
    {
        var observersLookup = observers.ToLookup(x => x.Message);
        var webhookUrl = _yandexMessengerBotOptions.WebhookUrl!;
        if (webhookUrl.PathAndQuery == context.Request.Path.Value)
        {
            var response = await JsonSerializer.DeserializeAsync<GetUpdateResponse>(context.Request.Body,
                YandexMessengerBotJsonOptions.Value);
            foreach (var update in response.Updates)
            {
                var globalObservers = observersLookup[string.Empty];
                foreach (var observer in globalObservers)
                {
                    await observer.OnNewUpdate(update);
                }

                var mesObservers = observersLookup[update.Text];
                foreach (var observer in mesObservers)
                {
                    await observer.OnNewUpdate(update);
                }
            }

            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "application/json";

            return;
        }

        await _next(context);
    }
}