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
    /// Indicates that a channel should be created.
    /// </summary>
    public bool? Channel { get; set; }

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
    /// <remarks>
    /// Should be empty if channel creating(Channel=true).
    /// </remarks>
    public User[]? Members { get; init; }

    /// <summary>
    /// Array of users which should be subscribed on the chat channel.
    /// </summary>
    /// <remarks>
    /// Should be empty if chat creating(Channel=false).
    /// </remarks>
    public User[]? Subscribers { get; init; }
}