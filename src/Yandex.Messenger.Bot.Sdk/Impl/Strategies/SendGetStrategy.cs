namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

/// <summary>
/// Generates a GET http request message without a payload.
/// </summary>
internal class SendGetStrategy : ISendStrategy
{
    private readonly string _endpoint;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendGetStrategy"/> class.
    /// </summary>
    /// <param name="endpoint">An endpoint relative url.</param>
    public SendGetStrategy(string endpoint)
    {
        _endpoint = endpoint;
    }

    /// <inheritdoc/>
    public HttpRequestMessage CreateRequest(object request)
    {
        return new HttpRequestMessage(HttpMethod.Get, _endpoint);
    }
}
