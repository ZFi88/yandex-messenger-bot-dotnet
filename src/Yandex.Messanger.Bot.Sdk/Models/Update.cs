namespace Yandex.Messanger.Bot.Sdk.Models;

public record Update(User From, Chat Chat, string Text, TimeSpan Timestamp, int MessageId, int UpdateId, File File);