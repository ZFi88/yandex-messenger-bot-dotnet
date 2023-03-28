namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

public record SendFileRequest : FileRequest
{
    public required string Filename { get; init; }

    public required Stream Document { get; init; }
}