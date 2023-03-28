namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

public record SendFileRequest(string? ChatId, string? Login) : FileRequest(ChatId, Login)
{
    public required string Filename { get; init; }

    public required Stream Document { get; init; }
}