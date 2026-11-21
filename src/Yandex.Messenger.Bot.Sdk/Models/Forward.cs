namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents messages which should be forwarded from one chat to another.
/// </summary>
/// <param name="ChatId">The ID of the source chat.</param>
/// <param name="MessageIds">The IDs of the messages which should be forwarded.</param>
public record Forward(string ChatId, long[] MessageIds);
