using Yandex.Messanger.Bot.Sdk.Abstractions;
using Yandex.Messanger.Bot.Sdk.Models.Requests;
using Yandex.Messanger.Bot.Sdk.Models.Responses;

namespace Yandex.Messanger.Bot.Sdk.Impl;

internal class Polls : BaseClient, IPolls
{
    public Polls(HttpClient client) : base(client)
    {
    }

    public async Task<CreatePollResponse> CreatePoll(CreatePollRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<CreatePollResponse>("messages/sendFile", HttpMethod.Post, request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<PollResultsResponse> GetPollResults(PollResultsRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<PollResultsResponse>("messages/sendFile", HttpMethod.Post, request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<GetVotersResponse> GetVoters(GetVotersRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<GetVotersResponse>("messages/sendFile", HttpMethod.Post, request, cancellationToken)
            .ConfigureAwait(false);
    }
}