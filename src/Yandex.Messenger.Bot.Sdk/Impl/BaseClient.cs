namespace Yandex.Messenger.Bot.Sdk.Impl;

using System.Net;
using System.Text;
using System.Text.Json;
using Exceptions;
using Json;
using Models.Responses;
using Strategies;

/// <summary>
/// The base client. Provides common functionality for sending requests and handling errors.
/// </summary>
internal abstract class BaseClient
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions? _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseClient"/> class.
    /// </summary>
    /// <param name="client">A http client.</param>
    protected BaseClient(HttpClient client)
    {
        _client = client;
        _options = YandexMessengerBotJsonOptions.Value;
    }

    /// <summary>
    /// Sends a request to Yandex Messenger Bot API endpoints.
    /// </summary>
    /// <typeparam name="TResp">The type of response data.</typeparam>
    /// <param name="strategy">The http request creation strategy.</param>
    /// <param name="payload">The http request payload.</param>
    /// <param name="stoppingToken">A cancellation token.</param>
    protected async Task<TResp> Send<TResp>(ISendStrategy strategy, object payload, CancellationToken stoppingToken)
        where TResp : Response
    {
        using var request = strategy.CreateRequest(payload);
        using var response = await _client
            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, stoppingToken)
            .ConfigureAwait(false);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            var errorBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            throw new BotException(
                $"Yandex Messenger Bot API returned status code {(int)response.StatusCode} {response.StatusCode}. Response body: {errorBody}");
        }

        using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        var data = await JsonSerializer.DeserializeAsync<TResp>(stream, _options, stoppingToken)
            .AsTask()
            .ConfigureAwait(false);
        if (data is not { Ok: true })
        {
            throw new BotException(data?.Description);
        }

        return data;
    }
}