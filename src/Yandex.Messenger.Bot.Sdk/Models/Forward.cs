namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a message which should be forwarded from one chat to another.
/// </summary>
/// <param name="ChatId">The ID of the source chat.</param>
/// <param name="MessageId">The ID of the message which should be forwarded.</param>
/// <remarks>
/// The API expects a single message per forward object even though the documentation describes
/// an array of message IDs. To forward several messages, pass several <see cref="Forward"/> objects.
/// </remarks>
public record Forward(string ChatId, long MessageId);
