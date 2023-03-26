namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

public record CreatePollResponse(bool Ok, string Description, long MessageId) : Response(Ok, Description);