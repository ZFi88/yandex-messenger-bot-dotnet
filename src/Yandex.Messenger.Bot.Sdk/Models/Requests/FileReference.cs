namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a reference to a previously uploaded file.
/// </summary>
/// <param name="FileId">The identifier of a previously uploaded file.</param>
public record FileReference(string FileId);
