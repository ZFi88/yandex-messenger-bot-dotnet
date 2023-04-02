namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

/// <inheritdoc />
/// <param name="ChatId">A chat ID.</param>
public record CreateChatResponse(bool Ok, string Description, string ChatId) : Response(Ok, Description);