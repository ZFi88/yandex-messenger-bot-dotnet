namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a Yandex Messenger user.
/// </summary>
/// <param name="Login">The user login. Absent for channel senders.</param>
public record User(string? Login)
{
    /// <summary>
    /// The user ID (GUID). Present in responses when the sender is a channel.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// The user display name.
    /// </summary>
    public string? DisplayName { get; init; }

    /// <summary>
    /// Indicates that the user is a bot.
    /// </summary>
    public bool Robot { get; init; }
}
