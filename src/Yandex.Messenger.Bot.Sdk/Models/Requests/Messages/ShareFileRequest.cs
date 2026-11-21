namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a file sharing request data.
/// </summary>
public record ShareFileRequest : SendRequest
{
    /// <summary>
    /// The reference to a previously uploaded file.
    /// </summary>
    public required FileReference Document { get; init; }

    /// <summary>
    /// The file name.
    /// </summary>
    public string? Filename { get; init; }
}
