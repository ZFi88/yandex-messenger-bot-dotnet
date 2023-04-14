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
using Services;

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

        _endpoint = "/" + options.Value!.WebhookEndpoint.Trim(' ', '/');
    }

    /// <summary>
    /// Handles ASPNET Core request pipeline step.
    /// </summary>
    /// <param name="context">The <see cref="HttpContent"/>.</param>
    /// <param name="updateHandler">Updates processor.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public async Task InvokeAsync(
        HttpContext context,
        IUpdateHandler updateHandler)
    {
        var cancellationToken = context.RequestAborted;

        if (_endpoint.Equals(context.Request.Path.Value, StringComparison.OrdinalIgnoreCase))
        {
            var response = await JsonSerializer.DeserializeAsync<GetUpdateResponse>(
                context.Request.Body,
                YandexMessengerBotJsonOptions.Value,
                cancellationToken);

            foreach (var update in response!.Updates)
            {
                updateHandler.Handle(update);
            }

            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "application/json";

            return;
        }

        await _next(context);
    }
}