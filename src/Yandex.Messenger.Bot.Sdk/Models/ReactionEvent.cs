namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a reaction event.
/// </summary>
/// <param name="MessageId">The ID of the message the reaction was added to or removed from.</param>
/// <param name="Reaction">The reaction.</param>
/// <param name="Action">The reaction action: <c>add</c> or <c>remove</c>.</param>
public record ReactionEvent(long MessageId, Reaction Reaction, string Action);
