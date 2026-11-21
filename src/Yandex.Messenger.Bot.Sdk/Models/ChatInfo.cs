namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents detailed information about a chat or a channel.
/// </summary>
/// <param name="Type">The chat type.</param>
/// <param name="Id">The chat or channel identifier.</param>
public record ChatInfo(ChatType Type, string Id)
{
    /// <summary>
    /// Indicates that the chat is a private chat.
    /// </summary>
    public bool? Private { get; init; }

    /// <summary>
    /// The chat or channel name. Only for <c>group</c> and <c>channel</c>.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// The chat or channel description. Only for <c>group</c> and <c>channel</c>.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// The chat invite hash. Only for <c>group</c> and <c>channel</c>.
    /// </summary>
    public string? InviteHash { get; init; }

    /// <summary>
    /// The chat invite link. Only for <c>group</c> and <c>channel</c>.
    /// </summary>
    public string? InviteLink { get; init; }

    /// <summary>
    /// The reactions which are available in the chat.
    /// </summary>
    public Reaction[]? AvailableReactions { get; init; }
}
