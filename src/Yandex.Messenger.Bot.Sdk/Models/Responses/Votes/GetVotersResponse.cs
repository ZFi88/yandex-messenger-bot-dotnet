namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

/// <inheritdoc />
/// <param name="AnswerId"></param>
/// <param name="VotedCount"></param>
/// <param name="Cursor"></param>
/// <param name="Votes"></param>
public record GetVotersResponse(bool Ok, string Description, int AnswerId, int VotedCount, int Cursor, Vote[] Votes)
    : Response(Ok, Description);