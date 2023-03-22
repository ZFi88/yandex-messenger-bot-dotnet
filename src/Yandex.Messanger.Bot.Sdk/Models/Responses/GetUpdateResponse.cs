namespace Yandex.Messanger.Bot.Sdk.Models.Responses;

internal record GetUpdateResponse(bool Ok, string Description, Update[] Updates) : Response(Ok, Description);