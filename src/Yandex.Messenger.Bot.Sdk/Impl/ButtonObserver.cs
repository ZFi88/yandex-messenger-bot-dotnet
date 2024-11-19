namespace Yandex.Messenger.Bot.Sdk.Impl;

using Abstractions;
using Models;

/// <summary>
/// Base class which represents a button observer.
/// </summary>
public abstract class ButtonObserver : IObserver
{
    /// <summary>
    /// The button ID.
    /// </summary>
    public abstract Guid? ButtonId { get; protected init; }

    /// <inheritdoc />
    public string? Message => ButtonId?.ToString() ?? Guid.Empty.ToString();

    /// <inheritdoc />
    public abstract Task OnNewUpdate(Update update, CancellationToken cancellationToken);
}