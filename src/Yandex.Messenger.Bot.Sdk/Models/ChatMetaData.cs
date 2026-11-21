namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a chat or a channel with extended metadata.
/// </summary>
/// <param name="Type">The chat type.</param>
/// <param name="Id">The chat identifier.</param>
public record ChatMetaData(ChatType Type, string Id)
{
    /// <summary>
    /// The chat or channel title. Only for <c>group</c> and <c>channel</c>.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// The chat or channel description. Only for <c>group</c> and <c>channel</c>.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// The interlocutor login. Only for <c>private</c>.
    /// </summary>
    public string? Username { get; init; }
}
