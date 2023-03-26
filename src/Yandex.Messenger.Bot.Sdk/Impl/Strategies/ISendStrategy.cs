namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

/// <summary>
/// An abstraction which represents a request creation strategy.
/// </summary>
internal interface ISendStrategy
{
    /// <summary>
    /// Creates the http request message.
    /// </summary>
    /// <param name="request">An object provides the request options and payload.</param>
    HttpRequestMessage CreateRequest(object request);
}