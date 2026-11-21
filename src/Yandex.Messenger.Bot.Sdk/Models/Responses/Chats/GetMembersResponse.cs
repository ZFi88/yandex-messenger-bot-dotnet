namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

using Models;

/// <inheritdoc />
/// <param name="Data">The chat or channel members.</param>
public record GetMembersResponse(bool Ok, string Description, ChatMember[] Data) : Response(Ok, Description);
