namespace Yandex.Messanger.Bot.Sdk.Models.Requests;

public record CreatePollRequest
{
    public string? ChatId { get; init; }

    public string? Login { get; init; }

    public required string Title { get; init; }

    public required string[] Answers { get; init; }

    public int MaxChoice { get; init; } = 1;

    public bool IsAnonymous { get; init; } = false;

    public string? PayloadId { get; init; }

    public int? ReplyMessageId { get; init; }

    public bool DisableNotification { get; init; } = false;

    public bool Important { get; init; } = false;

    public bool DisablePreview { get; init; } = false;
}