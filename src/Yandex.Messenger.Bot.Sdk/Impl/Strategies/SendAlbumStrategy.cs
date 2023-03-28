namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

using Models.Requests;

internal class SendAlbumStrategy : MultipartStrategy<SendAlbumRequest>
{
    protected override string Endpoint => "messages/sendGallery";

    protected override string FilePartName => "images";

    protected override HttpRequestMessage CreateRequestInner(SendAlbumRequest sendFileRequest)
    {
        var request = base.CreateRequestInner(sendFileRequest);

        var content = (MultipartFormDataContent)request.Content!;
        foreach (var kv in sendFileRequest.Images)
        {
            var streamContent = new StreamContent(kv.Value);
            content.Add(streamContent, FilePartName, kv.Key);
        }

        return request;
    }
}