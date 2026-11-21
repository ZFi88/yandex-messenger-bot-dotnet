namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a message unpinning request data.
/// </summary>
public record UnpinMessageRequest : Request
{
    /// <summary>
    /// The ID of the message which should be unpinned.
    /// </summary>
    public required long MessageId { get; init; }
}
