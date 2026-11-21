namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

using Models;

/// <summary>
/// Represents a reaction sending request data.
/// </summary>
public record SendReactionRequest : Request
{
    /// <summary>
    /// The ID of the message the reaction should be set to or removed from.
    /// </summary>
    public required long MessageId { get; init; }

    /// <summary>
    /// The reaction which should be set. Pass <see langword="null"/> to remove the bot reaction.
    /// </summary>
    public Reaction? Reaction { get; init; }
}
