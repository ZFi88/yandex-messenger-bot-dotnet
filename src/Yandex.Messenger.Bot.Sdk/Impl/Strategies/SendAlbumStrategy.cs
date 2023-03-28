namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

using Models.Requests;

internal class SendAlbumStrategy : MultipartStrategy<SendAlbumRequest>
{
    protected override HttpRequestMessage CreateRequestInner(SendAlbumRequest sendFileRequest)
    {
        var request = base.CreateRequestInner(sendFileRequest);

        var content = (MultipartFormDataContent)request.Content!;
        foreach (var kv in sendFileRequest.Images)
        {
            var streamContent = new StreamContent(kv.Value);
            content.Add(streamContent, "images", kv.Key);
        }

        return request;
    }
}