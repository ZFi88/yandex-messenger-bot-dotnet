namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

using Exceptions;
using Models.Requests;

internal abstract class MultipartStrategy<TRequest> : BaseStrategy<TRequest>
    where TRequest : FileRequest
{
    protected abstract string Endpoint { get; }

    protected abstract string FilePartName { get; }
    
    protected override HttpRequestMessage CreateRequestInner(TRequest sendFileRequest)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, Endpoint);
        request.Headers.ExpectContinue = false;
        var content = new MultipartFormDataContent();

        if (sendFileRequest is { ChatId: { }, Login: { } } ||
            (sendFileRequest.ChatId == null && sendFileRequest.Login == null))
        {
            throw new BotException();
        }

        if (sendFileRequest.ChatId != null)
        {
            content.Add(new StringContent(sendFileRequest.ChatId), "chat_id");
        }
        else
        {
            content.Add(new StringContent(sendFileRequest.Login!), "login");
        }

        request.Content = content;

        return request;
    }
}