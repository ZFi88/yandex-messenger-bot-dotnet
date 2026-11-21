namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a base request which sends a message to a chat or a user.
/// </summary>
public abstract record SendRequest : Request
{
    /// <summary>
    /// The ID of a message which should be edited.
    /// </summary>
    public long? MessageId { get; init; }

    /// <summary>
    /// The ID of a message which should be replied.
    /// </summary>
    public long? ReplyMessageId { get; init; }

    /// <summary>
    /// A fragment of the replied message which should be quoted.
    /// </summary>
    public string? ReplyQuote { get; init; }

    /// <summary>
    /// The messages which should be forwarded to the target chat.
    /// </summary>
    public Forward[]? Forwards { get; init; }

    /// <summary>
    /// Indicates that notifications of the message should be disabled.
    /// </summary>
    public bool DisableNotification { get; init; }

    /// <summary>
    /// Indicates that the message is important.
    /// </summary>
    public bool Important { get; init; }

    /// <summary>
    /// A keyboard with buttons under the message.
    /// </summary>
    public SuggestButtons? SuggestButtons { get; init; }

    /// <summary>
    /// The action buttons under the message.
    /// </summary>
    public ActionButtons? ActionButtons { get; init; }
}
