namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a file request data.
/// </summary>
public abstract record FileRequest
{
    /// <summary>
    /// A chat ID. 
    /// </summary>
    public string? ChatId { get; init; }

    /// <summary>
    /// A user login.
    /// </summary>
    public string? Login { get; init; }
}