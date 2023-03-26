namespace Yandex.Messenger.Bot.Sdk.Abstractions;

/// <summary>
/// An abstraction which represents a bot client.
/// </summary>
public interface IYandexMessengerBotClient : IDisposable
{
    /// <summary>
    /// A chats API part.
    /// </summary>
    public IChats Chats { get; }

    /// <summary>
    /// A pools API part.
    /// </summary>
    public IPolls Polls { get; }

    /// <summary>
    /// An update API part.
    /// </summary>
    public IUpdates Updates { get; }
}