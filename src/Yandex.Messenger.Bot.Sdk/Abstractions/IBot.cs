namespace Yandex.Messenger.Bot.Sdk.Abstractions;

using Models.Requests;
using Models.Responses;

/// <summary>
/// An abstraction which represents the bot API.
/// </summary>
public interface IBot
{
    /// <summary>
    /// Gets the bot information and settings.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<BotInfoResponse> GetSelf(CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the bot webhook and event settings.
    /// </summary>
    /// <param name="request">The bot settings update parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<BotInfoResponse> UpdateSelf(UpdateSelfRequest request, CancellationToken cancellationToken = default);
}
