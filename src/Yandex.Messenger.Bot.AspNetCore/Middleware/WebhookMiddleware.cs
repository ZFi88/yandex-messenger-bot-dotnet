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

/// <summary>
/// The middleware for handling webhooks from Yandex Messenger Bot API.
/// </summary>
internal class WebhookMiddleware
{
    private readonly RequestDelegate _next;

    private readonly string _endpoint;

    /// <summary>
    /// Initializes a new instance of the <see cref="WebhookMiddleware"/> class.
    /// </summary>
    /// <param name="next">A request delegate.</param>
    /// <param name="options">The Yandex Messenger Bot options.</param>
    public WebhookMiddleware(RequestDelegate next, IOptions<YandexMessengerBotOptions> options)
    {
        _next = next;
        if (options.Value?.WebhookEndpoint == null)
        {
            throw new BotException(
                $"{YandexMessengerBotOptions.SectionName}:{nameof(YandexMessengerBotOptions.WebhookEndpoint)} configuration parameter required");
        }

        _endpoint = options.Value!.WebhookEndpoint;
    }

    /// <summary>
    /// Handles ASPNET Core request pipeline step.
    /// </summary>
    /// <param name="context">The <see cref="HttpContent"/>.</param>
    /// <param name="observers">A collection of message observers.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public async Task InvokeAsync(
        HttpContext context,
        IEnumerable<IObserver> observers)
    {
        var cancellationToken = context.RequestAborted;
        var observersLookup = observers.ToLookup(x => x.Message);

        if (_endpoint.Equals(context.Request.Path.Value, StringComparison.OrdinalIgnoreCase))
        {
            var response = await JsonSerializer.DeserializeAsync<GetUpdateResponse>(
                context.Request.Body,
                YandexMessengerBotJsonOptions.Value,
                cancellationToken);

            foreach (var update in response!.Updates)
            {
                var globalObservers = observersLookup[string.Empty];
                foreach (var observer in globalObservers)
                {
                    await observer.OnNewUpdate(update, cancellationToken);
                }

                var mesObservers = observersLookup[update.Text];
                foreach (var observer in mesObservers)
                {
                    await observer.OnNewUpdate(update, cancellationToken);
                }
            }

            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "application/json";

            return;
        }

        await _next(context);
    }
}