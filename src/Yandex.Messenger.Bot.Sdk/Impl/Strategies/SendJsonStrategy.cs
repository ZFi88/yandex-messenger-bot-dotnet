namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

using System.Text;
using System.Text.Json;
using Yandex.Messenger.Bot.Sdk.Json;

/// <inheritdoc />
internal class SendJsonStrategy : ISendStrategy
{
    private readonly string _endpoint;
    private readonly HttpMethod _method;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendJsonStrategy"/> class.
    /// </summary>
    /// <param name="endpoint">An endpoint relative url.</param>
    /// <param name="method">The http method.</param>
    public SendJsonStrategy(string endpoint, HttpMethod? method = null!)
    {
        _endpoint = endpoint;
        _method = method ?? HttpMethod.Post;
    }

    /// <inheritdoc/>
    public HttpRequestMessage CreateRequest(object payload)
    {
        return new HttpRequestMessage(_method, _endpoint)
        {
            Content = new StringContent(
                JsonSerializer.Serialize(payload, YandexMessengerBotJsonOptions.Value),
                Encoding.UTF8,
                "application/json")
        };
    }
}