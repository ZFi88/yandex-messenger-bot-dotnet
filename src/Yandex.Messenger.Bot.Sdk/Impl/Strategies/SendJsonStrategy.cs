namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

using System.Text;
using System.Text.Json;
using Yandex.Messenger.Bot.Sdk.Json;

internal class SendJsonStrategy : ISendStrategy
{
    private readonly string _endpoint;

    public SendJsonStrategy(string endpoint)
    {
        _endpoint = endpoint;
    }

    public HttpRequestMessage CreateRequest(object payload) =>
        new HttpRequestMessage(HttpMethod.Post, _endpoint)
        {
            Content = new StringContent(
                JsonSerializer.Serialize(payload, YandexMessengerBotJsonOptions.Value),
                Encoding.UTF8,
                "application/json")
        };
}