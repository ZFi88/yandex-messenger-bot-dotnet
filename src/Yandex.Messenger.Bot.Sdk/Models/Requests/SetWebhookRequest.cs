namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

public record SetWebhookRequest
{
    public required string? WebhookUrl { get; init; }
}