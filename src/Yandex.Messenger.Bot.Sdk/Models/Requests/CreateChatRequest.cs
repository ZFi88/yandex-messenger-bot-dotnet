namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a chat creation request data.
/// </summary>
public record CreateChatRequest
{
    /// <summary>
    /// The name of the chat.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The description of the chat.
    /// </summary>
    public required string Description { get; init; }

    /// <summary>
    /// A url of the chat avatar.
    /// </summary>
    public Uri? AvatarUrl { get; init; }

    /// <summary>
    /// Array of administrators which should be added to the chat.
    /// </summary>
    public User[]? Admins { get; init; }

    /// <summary>
    /// Array of users which should be added to the chat.
    /// </summary>
    public User[]? Members { get; init; }
}