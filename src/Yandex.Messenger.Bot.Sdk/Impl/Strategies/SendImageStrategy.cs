namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

using Models.Requests;

/// <inheritdoc />
internal class SendImageStrategy : MultipartStrategy<SendFileRequest>
{
    /// <inheritdoc/>
    protected override string Endpoint => "messages/sendImage";

    /// <inheritdoc/>
    protected override string FilePartName => "image";

    /// <inheritdoc/>
    protected override HttpRequestMessage CreateRequestInner(SendFileRequest sendFileRequest)
    {
        var request = base.CreateRequestInner(sendFileRequest);

        var content = (MultipartFormDataContent)request.Content!;
        var streamContent = new StreamContent(sendFileRequest.Document);
        content.Add(streamContent, FilePartName, sendFileRequest.Filename);

        return request;
    }
}