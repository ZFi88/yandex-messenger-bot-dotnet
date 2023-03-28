namespace Yandex.Messenger.Bot.Sdk.Impl;

using System.Net;
using System.Text;
using System.Text.Json;
using Exceptions;
using Json;
using Models.Responses;
using Strategies;

internal abstract class BaseClient
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions? _options;

    protected BaseClient(HttpClient client)
    {
        _client = client;
        _options = YandexMessengerBotJsonOptions.Value;
    }

    protected async Task<TResp> Send<TResp>(ISendStrategy strategy, object payload, CancellationToken stoppingToken)
        where TResp : Response
    {
        var request = strategy.CreateRequest(payload);
        var response = await _client.SendAsync(request, stoppingToken);
        var stream = await response.Content.ReadAsStreamAsync(stoppingToken);
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new BotException();
        }

        var data = await JsonSerializer.DeserializeAsync<TResp>(stream, _options, stoppingToken).AsTask();
        if (data is not { Ok: true })
        {
            throw new BotException(data?.Description);
        }

        return data;
    }
}