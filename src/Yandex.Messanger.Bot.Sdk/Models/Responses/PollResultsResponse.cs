namespace Yandex.Messanger.Bot.Sdk.Models.Responses;

public record PollResultsResponse(bool Ok, string Description, Dictionary<int, int> Answers, int VotedCount) : Response(Ok, Description);