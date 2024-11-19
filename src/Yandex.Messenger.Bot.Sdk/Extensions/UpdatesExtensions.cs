namespace Yandex.Messenger.Bot.Sdk.Extensions;

using Abstractions;
using Impl;
using Models;

/// <summary>
/// The extensions for <see cref="IUpdates"/> objects.
/// </summary>
public static class UpdatesExtensions
{
    /// <summary>
    /// Subscribes on all messages.
    /// </summary>
    /// <param name="updates">An <see cref="IUpdates"/> object.</param>
    /// <param name="messageHandler">A message handler.</param>
    public static void Subscribe(this IUpdates updates, Func<Update, CancellationToken, Task> messageHandler)
    {
        updates.Subscribe(new Observer(null!, messageHandler));
    }

    /// <summary>
    /// Subscribes on messages with message text.
    /// </summary>
    /// <param name="updates">An <see cref="IUpdates"/> object.</param>
    /// <param name="msg">A message text.</param>
    /// <param name="messageHandler">The message handler.</param>
    public static void Subscribe(
        this IUpdates updates,
        string msg,
        Func<Update, CancellationToken, Task> messageHandler)
    {
        updates.Subscribe(new Observer(msg, messageHandler));
    }

    /// <summary>
    /// Subscribes on button click.
    /// </summary>
    /// <param name="updates">An <see cref="IUpdates"/> object.</param>
    /// <param name="buttonId">A button ID.</param>
    /// <param name="messageHandler">The message handler.</param>
    public static void Subscribe(
        this IUpdates updates,
        Guid buttonId,
        Func<Update, CancellationToken, Task> messageHandler)
    {
        updates.Subscribe(new ButtonObserverImpl(buttonId, messageHandler));
    }
}