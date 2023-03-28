namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

internal abstract class BaseStrategy<TRequest> : ISendStrategy
{
    public HttpRequestMessage CreateRequest(object request)
    {
        var r = (TRequest)request;
        return CreateRequestInner(r);
    }

    protected abstract HttpRequestMessage CreateRequestInner(TRequest sendFileRequest);
}