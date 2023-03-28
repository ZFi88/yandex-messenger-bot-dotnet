namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

using Models.Requests;

internal class SendFileStrategy : MultipartStrategy<SendFileRequest>
{
    protected override string Endpoint => "messages/sendFile";

    protected override string FilePartName => "document";

    protected override HttpRequestMessage CreateRequestInner(SendFileRequest sendFileRequest)
    {
        var request = base.CreateRequestInner(sendFileRequest);

        var content = (MultipartFormDataContent)request.Content!;
        var streamContent = new StreamContent(sendFileRequest.Document);
        content.Add(streamContent, FilePartName, sendFileRequest.Filename);

        return request;
    }
}