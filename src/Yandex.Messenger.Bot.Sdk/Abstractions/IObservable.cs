namespace Yandex.Messenger.Bot.Sdk.Abstractions;

/// <summary>
/// An abstraction which represents an observable object.
/// </summary>
public interface IObservable
{
    /// <summary>
    /// Subscribes a observer to a observable.
    /// </summary>
    /// <param name="observer">The observer.</param>
    void Subscribe(IObserver observer);
}