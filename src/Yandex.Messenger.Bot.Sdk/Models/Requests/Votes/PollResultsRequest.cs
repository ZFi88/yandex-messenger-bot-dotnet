namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a poll results request data.
/// </summary>
public record PollResultsRequest : Request
{
    /// <summary>
    /// The ID of the message of th epoll.
    /// </summary>
    public required long MessageId { get; init; }

    /// <summary>
    /// Hash of invite link, if the bot is not member of the chat.
    /// </summary>
    public string? InviteHash { get; init; }
}