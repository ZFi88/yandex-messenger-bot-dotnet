namespace Yandex.Messenger.Bot.Sdk.Abstractions;

using Models;

/// <summary>
/// An abstraction which represents a observer.
/// </summary>
public interface IObserver
{
    /// <summary>
    /// The observer subscription message text.
    /// </summary>
    public string? Message { get; }

    /// <summary>
    /// Invokes then a new message handled.
    /// </summary>
    /// <param name="update">An <see cref="Update"/> object.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task OnNewUpdate(Update update, CancellationToken cancellationToken);
}