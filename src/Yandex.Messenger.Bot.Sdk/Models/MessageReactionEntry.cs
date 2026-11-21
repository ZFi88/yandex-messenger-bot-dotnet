namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a reaction with the author.
/// </summary>
/// <param name="Reaction">The reaction.</param>
/// <param name="Timestamp">The reaction event ID. Used as a pagination offset.</param>
/// <param name="User">The user who set the reaction.</param>
public record MessageReactionEntry(Reaction Reaction, long Timestamp, User User);
