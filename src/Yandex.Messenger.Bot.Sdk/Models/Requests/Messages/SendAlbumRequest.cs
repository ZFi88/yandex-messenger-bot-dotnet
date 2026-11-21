namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <inheritdoc />
public record SendAlbumRequest : SendRequest
{
    /// <summary>
    /// The dictionary there key represents an image name and the value represents an image file data stream.
    /// </summary>
    public required Dictionary<string, Stream> Images { get; init; }

    /// <summary>
    /// The album caption text.
    /// </summary>
    public string? Text { get; init; }
}
