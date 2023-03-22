using Yandex.Messanger.Bot.Sdk.Models;

namespace Yandex.Messanger.Bot.Sdk.Abstractions;

public interface IObserver
{
    public string? Message { get; }
    Task OnNewUpdate(Update update);
}