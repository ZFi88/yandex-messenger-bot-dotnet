namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a chat or a channel member.
/// </summary>
/// <param name="Guid">The unique member identifier.</param>
/// <param name="Role">The member role: <c>admin</c>, <c>member</c> or <c>subscriber</c>.</param>
/// <param name="IsBot">Indicates that the member is a bot.</param>
public record ChatMember(string Guid, string Role, bool IsBot)
{
    /// <summary>
    /// The member login.
    /// </summary>
    public string? Login { get; init; }
}
