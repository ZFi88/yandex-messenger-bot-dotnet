namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a update.
/// </summary>
/// <param name="From">A use how made the update.</param>
/// <param name="Chat">A chat in which the update was created.</param>
/// <param name="Text">A text of a message.</param>
/// <param name="Timestamp">The timestamp of the update.</param>
/// <param name="MessageId">The message ID.</param>
/// <param name="UpdateId">The update ID.</param>
/// <param name="File">The file of the update.</param>
public record Update(
    User From,
    Chat Chat,
    string Text,
    DateTime Timestamp,
    long MessageId,
    long UpdateId,
    File File,
    ButtonData? CallbackData);