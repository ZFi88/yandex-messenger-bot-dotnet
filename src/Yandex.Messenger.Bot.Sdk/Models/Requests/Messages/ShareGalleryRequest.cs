namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

using Models;

/// <summary>
/// Represents an album sharing request data.
/// </summary>
public record ShareGalleryRequest : SendRequest
{
    /// <summary>
    /// The references to previously uploaded images.
    /// </summary>
    public required Image[] Images { get; init; }

    /// <summary>
    /// The album caption text.
    /// </summary>
    public string? Text { get; init; }
}
