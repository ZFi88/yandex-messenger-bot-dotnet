namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents an image attached to a message.
/// </summary>
/// <param name="FileId">The file identifier for downloading through the API.</param>
/// <param name="Width">The image width.</param>
/// <param name="Height">The image height.</param>
public record Image(string FileId, int Width, int Height)
{
    /// <summary>
    /// The file size in bytes. Specified for the original image only.
    /// </summary>
    public int? Size { get; init; }

    /// <summary>
    /// The original file name. Specified for the original image only.
    /// </summary>
    public string? Name { get; init; }
}
