namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

/// <inheritdoc />
/// <param name="ChatId">A chat ID.</param>
/// <param name="InviteHash">An invite link hash. Returned when the chat is created with the <c>public</c> flag.</param>
/// <param name="InviteLink">An invite link. Returned when the chat is created with the <c>public</c> flag.</param>
public record CreateChatResponse(bool Ok, string Description, string ChatId, string? InviteHash, string? InviteLink)
    : Response(Ok, Description);
