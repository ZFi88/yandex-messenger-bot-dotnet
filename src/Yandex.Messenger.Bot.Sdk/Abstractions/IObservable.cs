namespace Yandex.Messenger.Bot.Sdk.Abstractions;

public interface IObservable
{
    public IDictionary<string, IObserver> Observers { get; }

    void Subscribe(IObserver observer);
}