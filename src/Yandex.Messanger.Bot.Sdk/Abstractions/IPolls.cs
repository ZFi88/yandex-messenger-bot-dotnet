using Yandex.Messanger.Bot.Sdk.Models.Requests;
using Yandex.Messanger.Bot.Sdk.Models.Responses;

namespace Yandex.Messanger.Bot.Sdk.Abstractions;

public interface IPolls
{
    Task<CreatePollResponse> CreatePoll(CreatePollRequest request, CancellationToken cancellationToken = default);
    Task<PollResultsResponse> GetPollResults(PollResultsRequest request, CancellationToken cancellationToken = default);
    Task<GetVotersResponse> GetVoters(GetVotersRequest request, CancellationToken cancellationToken = default);
}