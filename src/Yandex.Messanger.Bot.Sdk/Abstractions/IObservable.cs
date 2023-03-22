namespace Yandex.Messanger.Bot.Sdk.Abstractions;

public interface IObservable
{
    void Subscribe(IObserver observer);
}