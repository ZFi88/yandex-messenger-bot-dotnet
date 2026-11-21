namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents an anonymous reaction counter used for channels.
/// </summary>
/// <param name="Reaction">The reaction.</param>
/// <param name="Count">The number of such reactions on the message.</param>
public record ReactionCount(Reaction Reaction, int Count);
