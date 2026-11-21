namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents the bot event settings.
/// </summary>
public record BotSettings
{
    /// <summary>
    /// Indicates that the bot should receive reaction events.
    /// </summary>
    public bool? GetReactions { get; init; }

    /// <summary>
    /// Indicates that the bot should receive group chat members change events.
    /// </summary>
    public bool? GetMembersChanged { get; init; }
}
