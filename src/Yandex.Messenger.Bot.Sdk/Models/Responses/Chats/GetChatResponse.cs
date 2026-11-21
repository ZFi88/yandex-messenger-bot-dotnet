namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

using Models;

/// <inheritdoc />
/// <param name="Data">The chat or channel information.</param>
public record GetChatResponse(bool Ok, string Description, ChatInfo Data) : Response(Ok, Description);
