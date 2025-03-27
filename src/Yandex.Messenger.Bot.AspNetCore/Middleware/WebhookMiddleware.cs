namespace Yandex.Messenger.Bot.AspNetCore.Middleware;

using System.Net;
using System.Text.Json;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Options;
using Sdk.Abstractions;
using Sdk.Exceptions;
using Sdk.Json;
using Sdk.Models;
using Sdk.Models.Responses;

/// <summary>
/// The middleware for handling webhooks from Yandex Messenger Bot API.
/// </summary>
[UsedImplicitly]
internal class WebhookMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<WebhookMiddleware> _logger;

    private readonly string _endpoint;

    /// <summary>
    /// Initializes a new instance of the <see cref="WebhookMiddleware"/> class.
    /// </summary>
    /// <param name="next">A request delegate.</param>
    /// <param name="options">The Yandex Messenger Bot options.</param>
    /// <param name="logger">A logger.</param>
    public WebhookMiddleware(
        RequestDelegate next,
        IOptions<YandexMessengerBotOptions> options,
        ILogger<WebhookMiddleware> logger)
    {
        _next = next;
        _logger = logger;
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
    /// <param name="updateProcessor">An updates processor.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    [UsedImplicitly]
    public async Task InvokeAsync(
        HttpContext context,
        IUpdateProcessor updateProcessor)
    {
        if (_endpoint.Equals(context.Request.Path.Value, StringComparison.OrdinalIgnoreCase))
        {
            await ProcessData(context, updateProcessor);

            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "application/json";

            return;
        }

        await _next(context);
    }

    private async Task ProcessData(HttpContext context, IUpdateProcessor updateProcessor)
    {
        try
        {
            var cancellationToken = context.RequestAborted;
            using var streamReader = new StreamReader(context.Request.Body);
            var body = await streamReader.ReadToEndAsync();

            if (!await TryProcessUpdates(updateProcessor, body, cancellationToken) |
                !await TryProcessSingleUpdate(updateProcessor, body, cancellationToken))
            {
                _logger.LogError("An error occurred in serializing webhook data. Received data:\n{body}", body);
            }
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An error occurred during webhook processing.");
        }
    }

    private async Task<bool> TryProcessUpdates(
        IUpdateProcessor updateProcessor,
        string body,
        CancellationToken cancellationToken)
    {
        var response = JsonSerializer.Deserialize<GetUpdateResponse>(body, YandexMessengerBotJsonOptions.Value);

        if (response == null)
        {
            return false;
        }

        foreach (var update in response.Updates)
        {
            await updateProcessor.Process(update, cancellationToken);
        }

        return true;
    }

    private async Task<bool> TryProcessSingleUpdate(
        IUpdateProcessor updateProcessor,
        string body,
        CancellationToken cancellationToken)
    {
        var update = JsonSerializer.Deserialize<Update>(body, YandexMessengerBotJsonOptions.Value);

        if (update == null)
        {
            return false;
        }

        await updateProcessor.Process(update, cancellationToken);
        return true;
    }
}