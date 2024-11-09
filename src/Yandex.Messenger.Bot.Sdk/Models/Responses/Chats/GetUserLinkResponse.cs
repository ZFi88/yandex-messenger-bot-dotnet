namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

/// <inheritdoc />
/// <param name="Id">A user id.</param>
/// <param name="ChatLink">A link for user chat.</param>
/// <param name="CallLink">A link call.</param>
public record GetUserLinkResponse(
    bool Ok,
    string Id,
    string ChatLink,
    string CallLink,
    string Description) : Response(Ok, Description);
