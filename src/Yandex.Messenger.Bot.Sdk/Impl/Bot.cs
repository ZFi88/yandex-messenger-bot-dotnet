namespace Yandex.Messenger.Bot.Sdk.Impl;

using Abstractions;
using Models.Requests;
using Models.Responses;
using Strategies;

/// <inheritdoc cref="IBot" />
internal class Bot : BaseClient, IBot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Bot"/> class.
    /// </summary>
    /// <param name="client">A http client.</param>
    public Bot(HttpClient client)
        : base(client)
    {
    }

    /// <inheritdoc/>
    public async Task<BotInfoResponse> GetSelf(CancellationToken cancellationToken = default)
    {
        return await Send<BotInfoResponse>(new SendGetStrategy("self/get"), new object(), cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<BotInfoResponse> UpdateSelf(
        UpdateSelfRequest request,
        CancellationToken cancellationToken = default)
    {
        return await Send<BotInfoResponse>(new SendJsonStrategy("self/update"), request, cancellationToken)
            .ConfigureAwait(false);
    }
}
