namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a message pinning request data.
/// </summary>
public record PinMessageRequest : Request
{
    /// <summary>
    /// The ID of the message which should be pinned.
    /// </summary>
    public required long MessageId { get; init; }
}
