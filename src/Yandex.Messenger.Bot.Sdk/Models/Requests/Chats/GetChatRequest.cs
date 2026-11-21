namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a chat information getting request data.
/// </summary>
public record GetChatRequest
{
    /// <summary>
    /// The target chat ID.
    /// </summary>
    public required string ChatId { get; init; }
}
