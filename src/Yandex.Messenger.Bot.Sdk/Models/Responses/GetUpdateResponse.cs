namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

/// <inheritdoc />
/// <param name="Updates">An array of updates.</param>
public record GetUpdateResponse(bool Ok, string Description, Update[] Updates) : Response(Ok, Description);