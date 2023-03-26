namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

public record PollResultsRequest
{
    public string? ChatId { get; init; }

    public string? Login { get; init; }

    public required long MessageId { get; init; }

    public string? InviteHash { get; init; }
}