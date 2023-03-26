namespace Yandex.Messenger.Bot.Sdk.Abstractions;

using Models.Requests;
using Models.Responses;

/// <summary>
/// An abstraction which represents a polls API. 
/// </summary>
public interface IPolls
{
    /// <summary>
    /// Creates a new poll.
    /// </summary>
    /// <param name="request">The poll creation request parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<CreatePollResponse> CreatePoll(CreatePollRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a poll results.
    /// </summary>
    /// <param name="request">The poll results request parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<PollResultsResponse> GetPollResults(PollResultsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a poll voters.
    /// </summary>
    /// <param name="request">The poll voters request parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<GetVotersResponse> GetVoters(GetVotersRequest request, CancellationToken cancellationToken = default);
}