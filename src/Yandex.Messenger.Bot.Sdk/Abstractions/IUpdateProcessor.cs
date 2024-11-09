namespace Yandex.Messenger.Bot.Sdk.Abstractions;

using Models;

/// <summary>
/// An abstraction which represents an update processor.
/// </summary>
public interface IUpdateProcessor
{
    /// <summary>
    /// Process the Update.
    /// </summary>
    /// <param name="update">The Update.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task Process(Update update, CancellationToken cancellationToken);
}