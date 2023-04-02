namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a poll results request data.
/// </summary>
public record PollResultsRequest
{
    /// <summary>
    /// A chat ID.
    /// </summary>
    public string? ChatId { get; init; }

    /// <summary>
    /// A user login.
    /// </summary>
    public string? Login { get; init; }

    /// <summary>
    /// The ID of the message of th epoll.
    /// </summary>
    public required long MessageId { get; init; }

    /// <summary>
    /// Hash of invite link, if the bot is not member of the chat.
    /// </summary>
    public string? InviteHash { get; init; }
}