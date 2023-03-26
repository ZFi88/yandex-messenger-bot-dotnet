namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

/// <summary>
/// An abstraction which represents concrete type of a request options and payload.
/// </summary>
/// <typeparam name="TRequest">The type of request options and payload.</typeparam>
internal abstract class BaseStrategy<TRequest> : ISendStrategy
{
    /// <inheritdoc/>
    public HttpRequestMessage CreateRequest(object request)
    {
        var r = (TRequest)request;
        return CreateRequestInner(r);
    }

    /// <inheritdoc cref="ISendStrategy.CreateRequest"/>/>
    protected abstract HttpRequestMessage CreateRequestInner(TRequest sendFileRequest);
}