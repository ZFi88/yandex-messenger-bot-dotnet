namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

using Models;

/// <inheritdoc />
/// <param name="Chat">The chat the message was unpinned in.</param>
public record UnpinMessageResponse(bool Ok, string Description, Chat? Chat) : Response(Ok, Description);
