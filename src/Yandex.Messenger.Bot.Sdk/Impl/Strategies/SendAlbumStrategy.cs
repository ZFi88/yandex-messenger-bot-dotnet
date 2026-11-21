namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

using Models.Requests;

/// <inheritdoc />
internal class SendAlbumStrategy : MultipartStrategy<SendAlbumRequest>
{
    /// <inheritdoc/>
    protected override string Endpoint => "messages/sendGallery";

    /// <inheritdoc/>
    protected override string FilePartName => "images";

    /// <inheritdoc/>
    protected override HttpRequestMessage CreateRequestInner(SendAlbumRequest sendAlbumRequest)
    {
        var request = base.CreateRequestInner(sendAlbumRequest);

        var content = (MultipartFormDataContent)request.Content!;
        MultipartContentBuilder.AddString(content, "text", sendAlbumRequest.Text);
        foreach (var kv in sendAlbumRequest.Images)
        {
            var streamContent = new StreamContent(kv.Value);
            content.Add(streamContent, FilePartName, kv.Key);
        }

        return request;
    }
}
