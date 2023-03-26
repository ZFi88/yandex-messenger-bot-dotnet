namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

using Models.Requests;

/// <inheritdoc />
internal class SendFileStrategy : MultipartStrategy<SendFileRequest>
{
    /// <inheritdoc/>
    protected override string Endpoint => "messages/sendFile";

    /// <inheritdoc/>
    protected override string FilePartName => "document";

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