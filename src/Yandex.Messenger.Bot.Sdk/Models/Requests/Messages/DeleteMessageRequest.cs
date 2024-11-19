namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a delete message request data.
/// </summary>
public record DeleteMessageRequest : Request
{
    /// <summary>
    /// A message ID.
    /// </summary>
    public required int MessageId { get; init; }
}