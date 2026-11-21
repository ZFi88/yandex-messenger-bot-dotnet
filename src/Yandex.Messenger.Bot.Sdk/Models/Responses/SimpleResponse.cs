namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

/// <inheritdoc />
public record SimpleResponse(bool Ok, string Description) : Response(Ok, Description);
