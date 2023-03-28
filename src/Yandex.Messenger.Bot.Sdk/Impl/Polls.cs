namespace Yandex.Messenger.Bot.Sdk.Impl;

using Abstractions;
using Models.Requests;
using Models.Responses;
using Strategies;

internal class Polls : BaseClient, IPolls
{
    public Polls(HttpClient client)
        : base(client)
    {
    }

    public async Task<CreatePollResponse> CreatePoll(CreatePollRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<CreatePollResponse>(new SendJsonStrategy("messages/sendFile"), request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<PollResultsResponse> GetPollResults(PollResultsRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<PollResultsResponse>(new SendJsonStrategy("messages/sendFile"), request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<GetVotersResponse> GetVoters(GetVotersRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<GetVotersResponse>(new SendJsonStrategy("messages/sendFile"), request, cancellationToken)
            .ConfigureAwait(false);
    }
}