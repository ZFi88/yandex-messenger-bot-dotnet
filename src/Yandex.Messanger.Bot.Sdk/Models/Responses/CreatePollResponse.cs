namespace Yandex.Messanger.Bot.Sdk.Models.Responses;

public record CreatePollResponse(bool Ok, string Description, int MessageId) : Response(Ok, Description);