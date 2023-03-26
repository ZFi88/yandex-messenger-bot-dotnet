namespace Yandex.Messenger.Bot.Sdk.Models;

public record Update(User From, Chat Chat, string Text, DateTime Timestamp, long MessageId, long UpdateId, File File);