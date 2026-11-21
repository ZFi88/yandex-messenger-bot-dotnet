namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

using Models;

/// <inheritdoc />
/// <param name="Data">The chats and channels list.</param>
public record GetChatsResponse(bool Ok, string Description, ChatMetaData[] Data) : Response(Ok, Description);
