namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a getting user links request.
/// </summary>
public record GetUserLinkRequest
{
    /// <summary>
    /// A user login.
    /// </summary>
    public required string Login { get; set; }
}