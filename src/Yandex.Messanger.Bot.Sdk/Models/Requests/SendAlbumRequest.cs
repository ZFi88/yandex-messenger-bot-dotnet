namespace Yandex.Messanger.Bot.Sdk.Models.Requests;

public record SendAlbumRequest
{
    public string? ChatId { get; init; }
    public string? Login { get; init; }
    public required Stream[] Images { get; init; }
}