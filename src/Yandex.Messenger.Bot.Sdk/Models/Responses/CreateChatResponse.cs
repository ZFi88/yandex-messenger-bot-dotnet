namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

public record CreateChatResponse(bool Ok, string Description, string ChatId) : Response(Ok, Description);