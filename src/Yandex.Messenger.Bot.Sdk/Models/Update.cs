namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a chat update data.
/// </summary>
/// <param name="From">The user that made the update.</param>
/// <param name="Chat">The chat the update is created in.</param>
/// <param name="Text">The message text. Absent for non-text updates.</param>
/// <param name="Timestamp">The update timestamp.</param>
/// <param name="MessageId">The message ID.</param>
/// <param name="UpdateId">The update ID.</param>
/// <param name="File">The attached file. Absent if the message has no file.</param>
/// <param name="CallbackData">The legacy inline button callback data.</param>
public record Update(
    User From,
    Chat Chat,
    string? Text,
    DateTime Timestamp,
    long MessageId,
    long UpdateId,
    File? File,
    ButtonData? CallbackData)
{
    /// <summary>
    /// The images attached to the message.
    /// </summary>
    public Image[][]? Images { get; init; }

    /// <summary>
    /// The forwarded messages.
    /// </summary>
    public Update[]? ForwardedMessages { get; init; }

    /// <summary>
    /// The message the current message replies to.
    /// </summary>
    public Update? ReplyToMessage { get; init; }

    /// <summary>
    /// The sticker attached to the message.
    /// </summary>
    public Sticker? Sticker { get; init; }

    /// <summary>
    /// The group chat members change event.
    /// </summary>
    public ChatMembersUpdate? ChatMembersUpdate { get; init; }

    /// <summary>
    /// The reaction event.
    /// </summary>
    public ReactionEvent? Reaction { get; init; }

    /// <summary>
    /// The request to the bot produced by a suggest button.
    /// </summary>
    public BotRequest? BotRequest { get; init; }
}
