namespace Yandex.Messenger.Bot.Sdk.Abstractions;

/// <summary>
/// Represents an observable object.
/// </summary>
public interface IObservable
{
    /// <summary>
    /// Subscribes an observer to an observable.
    /// </summary>
    /// <param name="observer">The observer.</param>
    void Subscribe(IObserver observer);
}