namespace Yandex.Messanger.Bot.Sdk.Abstractions;

using Models;

public interface IObserver
{
    public string? Message { get; }

    Task OnNewUpdate(Update update);
}