namespace Yandex.Messenger.Bot.Sdk.Abstractions;

using Models;

/// <summary>
/// Represents a message observer.
/// </summary>
public interface IObserver
{
    /// <summary>
    /// The value when <see cref="Update"/> text equals to, the observer handles the <see cref="Update"/> object.
    /// </summary>
    public string? Message { get; }

    /// <summary>
    /// Handles a new <see cref="Update"/> object.
    /// </summary>
    /// <param name="update">An <see cref="Update"/> object.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task OnNewUpdate(Update update, CancellationToken cancellationToken);
}