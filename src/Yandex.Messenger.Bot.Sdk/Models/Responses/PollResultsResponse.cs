namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

/// <inheritdoc />
/// <param name="Answers">The count of answers by answers id.</param>
/// <param name="VotedCount">The count of votes.</param>
public record PollResultsResponse(bool Ok, string Description, Dictionary<int, int> Answers, int VotedCount)
    : Response(Ok, Description);