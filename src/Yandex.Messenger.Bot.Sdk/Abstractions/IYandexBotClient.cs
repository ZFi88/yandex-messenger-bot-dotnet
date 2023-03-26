namespace Yandex.Messenger.Bot.Sdk.Abstractions;

public interface IYandexBotClient
{
    public IChats Chats { get; }

    public IPolls Polls { get; }

    public IUpdates Updates { get; }
}