namespace Yandex.Messenger.Bot.Sdk.Impl;

using Abstractions;
using Models;

internal class Observer : IObserver
{
    private readonly Func<Update, Task> _func;

    public Observer(string? message, Func<Update, Task> func)
    {
        Message = message;
        _func = func;
    }

    public string? Message { get; }

    public Task OnNewUpdate(Update update)
    {
        return _func.Invoke(update);
    }
}