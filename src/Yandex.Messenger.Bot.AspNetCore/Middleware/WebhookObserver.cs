namespace Yandex.Messenger.Bot.AspNetCore.Middleware;

using Sdk.Abstractions;
using Sdk.Models;

/// <summary>
/// The common observer for handling updates from a webhook.
/// </summary>
internal class WebhookObserver : IObserver
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Func<IServiceProvider, Update, CancellationToken, Task> _messageHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="WebhookObserver"/> class.
    /// </summary>
    /// <param name="serviceProvider">A DI container.</param>
    /// <param name="message">The text of a message for observing.
    /// If null or string.Empty the observer will handle all updates.</param>
    /// <param name="messageHandler">A function for updates handling.</param>
    public WebhookObserver(
        IServiceProvider serviceProvider,
        string? message,
        Func<IServiceProvider, Update, CancellationToken, Task> messageHandler)
    {
        Message = message;
        _serviceProvider = serviceProvider;
        _messageHandler = messageHandler;
    }

    /// <inheritdoc/>
    public string? Message { get; }

    /// <inheritdoc/>
    public Task OnNewUpdate(Update update, CancellationToken cancellationToken)
    {
        return _messageHandler.Invoke(_serviceProvider, update, cancellationToken);
    }
}