namespace Yandex.Messenger.Bot.Sdk.Impl;

using Abstractions;
using Models;

internal class Observer : IObserver
{
    private readonly Func<Update, CancellationToken, Task> _func;

    public Observer(string? message, Func<Update, CancellationToken, Task> func)
    {
        Message = message;
        _func = func;
    }

    public string? Message { get; }

    public Task OnNewUpdate(Update update, CancellationToken cancellationToken)
    {
        return _func.Invoke(update, cancellationToken);
    }
}