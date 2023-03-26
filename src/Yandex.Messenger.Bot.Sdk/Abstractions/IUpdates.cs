namespace Yandex.Messenger.Bot.Sdk.Abstractions;

using Models.Requests;
using Models.Responses;

/// <summary>
/// An abstraction which represents an updates API.
/// </summary>
public interface IUpdates : IObservable
{
    /// <summary>
    /// Gets an updates for a bot.
    /// </summary>
    /// <param name="request">The update request parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<Response> GetUpdates(GetUpdateRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Set a webhook for updates handling.
    /// </summary>
    /// <param name="request">The webhook setting request parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<SetWebhookResponse> SetWebhook(SetWebhookRequest request, CancellationToken cancellationToken = default);
}