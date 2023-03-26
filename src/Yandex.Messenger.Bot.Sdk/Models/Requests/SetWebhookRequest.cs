namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

public record SetWebhookRequest
{
    public string? WebhookUrl { get; init; }
}