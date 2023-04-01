namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a file.
/// </summary>
/// <param name="Id">The file ID.</param>
/// <param name="Name">The file name.</param>
/// <param name="Size">The file size.</param>
public record File(string Id, string Name, int Size);