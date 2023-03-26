namespace Yandex.Messenger.Bot.Sdk.Impl;

using Abstractions;
using Models;

/// <inheritdoc />
internal class Observer : IObserver
{
    private readonly Func<Update, CancellationToken, Task> _messageHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="Observer"/> class.
    /// </summary>
    /// <param name="message">A text of a message which triggers an observer.</param>
    /// <param name="messageHandler">The message handler.</param>
    public Observer(string? message, Func<Update, CancellationToken, Task> messageHandler)
    {
        Message = message;
        _messageHandler = messageHandler;
    }

    /// <inheritdoc/>
    public string? Message { get; }

    /// <inheritdoc/>
    public Task OnNewUpdate(Update update, CancellationToken cancellationToken)
    {
        return _messageHandler.Invoke(update, cancellationToken);
    }
}