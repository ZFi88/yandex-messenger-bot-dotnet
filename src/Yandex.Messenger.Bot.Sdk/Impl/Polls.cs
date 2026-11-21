namespace Yandex.Messenger.Bot.Sdk.Impl;

using Abstractions;
using Models.Requests;
using Models.Responses;
using Strategies;

/// <inheritdoc cref="IPolls"/> />
internal class Polls : BaseClient, IPolls
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Polls"/> class.
    /// </summary>
    /// <param name="client">A http client.</param>
    public Polls(HttpClient client)
        : base(client)
    {
    }

    /// <inheritdoc/>
    public async Task<CreatePollResponse> CreatePoll(CreatePollRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<CreatePollResponse>(new SendJsonStrategy("messages/createPoll"), request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<PollResultsResponse> GetPollResults(PollResultsRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<PollResultsResponse>(
                new SendJsonToQueryStringStrategy("polls/getResults"),
                request,
                cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<GetVotersResponse> GetVoters(GetVotersRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<GetVotersResponse>(
                new SendJsonToQueryStringStrategy("polls/getVoters"),
                request,
                cancellationToken)
            .ConfigureAwait(false);
    }
}