namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a system message sending request data.
/// </summary>
public record SendSystemMessageRequest : Request
{
    /// <summary>
    /// The system message text.
    /// </summary>
    public required string Text { get; init; }

    /// <summary>
    /// Indicates that notifications of the message should be disabled.
    /// </summary>
    public bool DisableNotification { get; init; }

    /// <summary>
    /// A payload ID.
    /// </summary>
    public string? PayloadId { get; init; }
}
