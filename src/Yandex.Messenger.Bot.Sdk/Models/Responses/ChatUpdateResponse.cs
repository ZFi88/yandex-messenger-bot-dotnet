namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

public record ChatUpdateResponse(bool Ok, string Description) : Response(Ok, Description);