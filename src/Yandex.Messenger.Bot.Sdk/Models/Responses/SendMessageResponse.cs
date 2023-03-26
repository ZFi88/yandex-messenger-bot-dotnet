namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

public record SendMessageResponse(bool Ok, string Description, long MessageId) : Response(Ok, Description);