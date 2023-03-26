namespace Yandex.Messenger.Bot.AspNetCore.Middleware;

using Sdk.Abstractions;
using Sdk.Models;

internal class WebhookObserver : IObserver
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Func<IServiceProvider, Update, Task> _messageHandler;

    public WebhookObserver(
        IServiceProvider serviceProvider,
        string? message,
        Func<IServiceProvider, Update, Task> messageHandler)
    {
        Message = message;
        _serviceProvider = serviceProvider;
        _messageHandler = messageHandler;
    }

    public string? Message { get; }

    public Task OnNewUpdate(Update update)
    {
        return _messageHandler.Invoke(_serviceProvider, update);
    }
}