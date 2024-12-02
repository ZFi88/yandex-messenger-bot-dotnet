namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a chat update data.
/// </summary>
/// <param name="From">The user that made the update.</param>
/// <param name="Chat">The chat the update is created in.</param>
/// <param name="Text">The message text.</param>
/// <param name="Timestamp">The update timestamp.</param>
/// <param name="MessageId">The message ID.</param>
/// <param name="UpdateId">The update ID.</param>
/// <param name="File">The attached file.</param>
public record Update(
    User From,
    Chat Chat,
    string Text,
    DateTime Timestamp,
    long MessageId,
    long UpdateId,
    File File,
    ButtonData? CallbackData);