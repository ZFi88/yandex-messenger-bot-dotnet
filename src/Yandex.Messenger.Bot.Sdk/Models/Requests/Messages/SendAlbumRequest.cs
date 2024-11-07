namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <inheritdoc />
public record SendAlbumRequest : Request
{
    /// <summary>
    /// The dictionary there key represents an image name and the value represents an image file data stream.
    /// </summary>
    public required Dictionary<string, Stream> Images { get; init; }
}