namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a chat.
/// </summary>
/// <param name="Id">The chat ID.</param>
/// <param name="Type">The chat type.</param>
public record Chat(string Id, ChatType Type);