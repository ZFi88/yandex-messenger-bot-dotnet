namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

/// <inheritdoc />
public record ChatUpdateResponse(bool Ok, string Description) : Response(Ok, Description);