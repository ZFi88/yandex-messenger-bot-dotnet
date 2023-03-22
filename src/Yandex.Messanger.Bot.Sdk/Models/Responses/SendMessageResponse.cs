namespace Yandex.Messanger.Bot.Sdk.Models.Responses;

public record SendMessageResponse(bool Ok, string Description, int MessageId) : Response(Ok, Description);