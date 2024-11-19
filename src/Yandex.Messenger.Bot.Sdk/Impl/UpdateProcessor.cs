namespace Yandex.Messenger.Bot.Sdk.Impl;

using Abstractions;
using Models;

/// <inheritdoc />
internal class UpdateProcessor : IUpdateProcessor
{
    private readonly Dictionary<string, List<IObserver>> _observers = new();

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="observers">A list of observers.</param>
    public UpdateProcessor(IEnumerable<IObserver> observers)
    {
        foreach (var observer in observers)
        {
            Add(observer);
        }
    }

    /// <inheritdoc />
    public async Task Process(Update update, CancellationToken cancellationToken)
    {
        if (_observers.TryGetValue(string.Empty, out var globalObservers))
        {
            foreach (var observer in globalObservers)
            {
                await observer.OnNewUpdate(update, cancellationToken);
            }
        }

        if (update.CallbackData != null &&
            _observers.TryGetValue(update.CallbackData.Id.ToString(), out var buttonObservers))
        {
            foreach (var observer in buttonObservers)
            {
                await observer.OnNewUpdate(update, cancellationToken);
            }
        }

        if (_observers.TryGetValue(update.Text, out var msgObservers))
        {
            foreach (var observer in msgObservers)
            {
                await observer.OnNewUpdate(update, cancellationToken);
            }
        }
    }

    /// <summary>
    /// Adds an observer.
    /// </summary>
    /// <param name="observer">The observer.</param>
    public void Add(IObserver observer)
    {
        var key = observer.Message ?? string.Empty;
        _observers.TryGetValue(key, out var observers);

        if (observers != null)
        {
            observers.Add(observer);
        }
        else
        {
            _observers.Add(key, new List<IObserver>() { observer });
        }
    }
}