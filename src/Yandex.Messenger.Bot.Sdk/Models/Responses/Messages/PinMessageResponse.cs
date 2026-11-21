namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

using Models;

/// <inheritdoc />
/// <param name="MessageId">The ID of the pinned message.</param>
/// <param name="Chat">The chat the message was pinned in.</param>
public record PinMessageResponse(bool Ok, string Description, long MessageId, Chat? Chat) : Response(Ok, Description);
