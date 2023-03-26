namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

public record GetUpdateResponse(bool Ok, string Description, Update[] Updates) : Response(Ok, Description);