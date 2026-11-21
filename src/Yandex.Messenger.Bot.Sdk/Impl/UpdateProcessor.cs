namespace Yandex.Messenger.Bot.Sdk.Impl;

using Abstractions;
using Microsoft.Extensions.Logging;
using Models;

/// <summary>
/// Represents an <see cref="Update"/> processor.
/// </summary>
internal class UpdateProcessor : IUpdateProcessor
{
    private readonly Dictionary<string, List<IObserver>> _observers = new();
    private readonly object _lock = new();
    private readonly ILogger<UpdateProcessor>? _logger;

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="observers">A list of observers.</param>
    /// <param name="logger">An optional logger for observer exception diagnostics.</param>
    public UpdateProcessor(IEnumerable<IObserver> observers, ILogger<UpdateProcessor>? logger = null)
    {
        _logger = logger;
        foreach (var observer in observers)
        {
            Add(observer);
        }
    }

    /// <inheritdoc />
    public async Task Process(Update update, CancellationToken cancellationToken)
    {
        List<IObserver>? globalSnapshot = null;
        List<IObserver>? buttonSnapshot = null;
        List<IObserver>? messageSnapshot = null;

        lock (_lock)
        {
            if (_observers.TryGetValue(string.Empty, out var globalObservers))
            {
                globalSnapshot = globalObservers.ToList();
            }

            if (update.CallbackData != null &&
                _observers.TryGetValue(update.CallbackData.Id.ToString(), out var buttonObservers))
            {
                buttonSnapshot = buttonObservers.ToList();
            }

            if (!string.IsNullOrEmpty(update.Text) && _observers.TryGetValue(update.Text, out var msgObservers))
            {
                messageSnapshot = msgObservers.ToList();
            }
        }

        await NotifyObservers(globalSnapshot, update, cancellationToken).ConfigureAwait(false);
        await NotifyObservers(buttonSnapshot, update, cancellationToken).ConfigureAwait(false);
        await NotifyObservers(messageSnapshot, update, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Adds an observer.
    /// </summary>
    /// <param name="observer">The observer.</param>
    public void Add(IObserver observer)
    {
        var key = observer.Message ?? string.Empty;
        lock (_lock)
        {
            if (_observers.TryGetValue(key, out var observers))
            {
                observers.Add(observer);
            }
            else
            {
                _observers.Add(key, new List<IObserver>() { observer });
            }
        }
    }

    /// <summary>
    /// Notifies a list of observers about an update, swallowing exceptions from individual
    /// observers so that one failing observer does not prevent the remaining observers
    /// from being notified. When a logger is configured, swallowed exceptions are logged
    /// for diagnostics.
    /// </summary>
    /// <param name="observers">The observers to notify.</param>
    /// <param name="update">The update to send.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    private async Task NotifyObservers(
        List<IObserver>? observers,
        Update update,
        CancellationToken cancellationToken)
    {
        if (observers is null)
        {
            return;
        }

        foreach (var observer in observers)
        {
            try
            {
                await observer.OnNewUpdate(update, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                // Swallow exceptions from individual observers so one failing observer
                // does not interrupt notification of the remaining observers.
                _logger?.LogError(
                    e,
                    "Observer {ObserverType} threw an exception while processing update.",
                    observer.GetType().Name);
            }
        }
    }
}
