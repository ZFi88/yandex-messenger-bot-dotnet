namespace Yandex.Messanger.Bot.Sdk.Abstractions;

using Models.Requests;
using Models.Responses;

public interface IPolls
{
    Task<CreatePollResponse> CreatePoll(CreatePollRequest request, CancellationToken cancellationToken = default);
    Task<PollResultsResponse> GetPollResults(PollResultsRequest request, CancellationToken cancellationToken = default);
    Task<GetVotersResponse> GetVoters(GetVotersRequest request, CancellationToken cancellationToken = default);
}