namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

internal record GetUpdateResponse(bool Ok, string Description, Update[] Updates) : Response(Ok, Description);