namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents base request.
/// </summary>
public abstract record Request
{
    /// <summary>
    /// A chat ID.
    /// </summary>
    public string? ChatId { get; init; }

    /// <summary>
    /// A user login.
    /// </summary>
    public string? Login { get; init; }

    /// <summary>
    /// The ID of a thread.
    /// </summary>
    public int? ThreadId { get; init; } = default;
}