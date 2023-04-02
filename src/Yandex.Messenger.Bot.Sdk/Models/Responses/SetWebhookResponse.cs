namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

/// <inheritdoc />
public record SetWebhookResponse(bool Ok, string Description) : Response(Ok, Description);