namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

public record SendMessageRequest
{
    public string? ChatId { get; init; }

    public string? Login { get; init; }

    public required string Text { get; init; }

    public string? PayloadId { get; init; }

    public string? ReplyMessageId { get; init; }

    public bool DisableNotification { get; init; } = false;

    public bool Important { get; init; } = false;

    public bool DisablePreview { get; init; } = false;
}