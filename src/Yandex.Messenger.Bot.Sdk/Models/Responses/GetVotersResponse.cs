namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

public record GetVotersResponse(bool Ok, string Description, int AnswerId, int VotedCount, int Cursor, Vote[] Votes)
    : Response(Ok, Description);