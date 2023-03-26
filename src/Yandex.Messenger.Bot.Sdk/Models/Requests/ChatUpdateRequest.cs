namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a chat update request data.
/// </summary>
public record ChatUpdateRequest
{
    /// <summary>
    /// The chat ID.
    /// </summary>
    public required string ChatId { get; init; }

    /// <summary>
    /// Array of users which should be added to the chat.
    /// </summary>
    public User[] Members { get; init; } = Array.Empty<User>();

    /// <summary>
    /// Array of administrators which should be added to the chat.
    /// </summary>
    public User[] Admins { get; init; } = Array.Empty<User>();

    /// <summary>
    /// Array of users which should be subscribed to the chat updates.
    /// </summary>
    public User[] Subscribers { get; init; } = Array.Empty<User>();

    /// <summary>
    /// Array of users which should be deleted from the chat.
    /// </summary>
    public User[] Remove { get; init; } = Array.Empty<User>();
}