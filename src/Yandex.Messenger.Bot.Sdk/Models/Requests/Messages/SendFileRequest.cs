namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <inheritdoc />
public record SendFileRequest : Request
{
    /// <summary>
    /// A file name.
    /// </summary>
    public required string Filename { get; init; }

    /// <summary>
    /// The file data stream.
    /// </summary>
    public required Stream Document { get; init; }
}