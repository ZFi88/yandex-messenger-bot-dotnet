namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a webhook setting request data.
/// </summary>
public record SetWebhookRequest
{
    /// <summary>
    /// The webhook absolute url.
    /// </summary>
    public required string? WebhookUrl { get; init; }
}