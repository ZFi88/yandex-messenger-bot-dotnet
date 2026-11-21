namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a file getting request data.
/// </summary>
public record GetFileRequest
{
    /// <summary>
    /// The file ID.
    /// </summary>
    public required string FileId { get; init; }
}
