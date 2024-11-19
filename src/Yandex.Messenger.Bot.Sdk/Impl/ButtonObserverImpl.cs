namespace Yandex.Messenger.Bot.Sdk.Impl;

using Models;

/// <inheritdoc />
internal class ButtonObserverImpl : ButtonObserver
{
    private readonly Func<Update, CancellationToken, Task> _messageHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="ButtonObserverImpl"/> class.
    /// </summary>
    /// <param name="buttonId">The button ID.</param>
    /// <param name="messageHandler">The message handler.</param>
    public ButtonObserverImpl(Guid buttonId, Func<Update, CancellationToken, Task> messageHandler)
    {
        ButtonId = buttonId;
        _messageHandler = messageHandler;
    }

    /// <inheritdoc/>
    public sealed override Guid? ButtonId { get; protected init; }

    /// <inheritdoc/>
    public override Task OnNewUpdate(Update update, CancellationToken cancellationToken)
    {
        return _messageHandler.Invoke(update, cancellationToken);
    }
}