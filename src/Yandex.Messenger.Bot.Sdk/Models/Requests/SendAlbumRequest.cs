namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

public record SendAlbumRequest(string? ChatId, string? Login) : FileRequest(ChatId, Login)
{
    public required Dictionary<string, Stream> Images { get; init; }
}