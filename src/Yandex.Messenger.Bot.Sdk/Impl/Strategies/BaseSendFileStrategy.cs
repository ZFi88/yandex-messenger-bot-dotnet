namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

using Models.Requests;

internal abstract class BaseSendFileStrategy<TRequest> : MultipartStrategy<TRequest>
    where TRequest : FileRequest
{
    protected abstract string Endpoint { get; }

    protected abstract string FilePartName { get; }
}