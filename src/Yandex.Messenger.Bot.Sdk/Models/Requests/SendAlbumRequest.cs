namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

public record SendAlbumRequest : FileRequest
{
    public required Dictionary<string, Stream> Images { get; init; }
}