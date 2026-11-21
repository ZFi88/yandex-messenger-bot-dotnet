namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a sticker attached to a message.
/// </summary>
/// <param name="Id">The sticker ID.</param>
/// <param name="SetId">The sticker set ID.</param>
public record Sticker(string Id, string SetId);
