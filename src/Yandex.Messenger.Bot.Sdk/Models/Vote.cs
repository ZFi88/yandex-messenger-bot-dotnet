namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a vote.
/// </summary>
/// <param name="Timestamp">The timestamp of the vote.</param>
/// <param name="User">The user of the vote.</param>
public record Vote(DateTime Timestamp, User User);