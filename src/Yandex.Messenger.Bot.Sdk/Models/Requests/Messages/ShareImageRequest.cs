namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

using Models;

/// <summary>
/// Represents an image sharing request data.
/// </summary>
public record ShareImageRequest : SendRequest
{
    /// <summary>
    /// The reference to a previously uploaded image.
    /// </summary>
    public required Image Image { get; init; }

    /// <summary>
    /// The file name.
    /// </summary>
    public string? Filename { get; init; }
}
