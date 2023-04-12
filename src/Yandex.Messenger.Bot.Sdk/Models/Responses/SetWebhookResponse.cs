namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

/// <inheritdoc />
public record SetWebhookResponse(
        bool Ok,
        string Description,
        string Id,
        string DisplayName,
        object WebhookUrl,
        long[] Organizations,
        string Login)
    : Response(Ok, Description);
