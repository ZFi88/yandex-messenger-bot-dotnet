namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a webhook setting request data.
/// </summary>
public record SetWebhookRequest
{
    /// <summary>
    /// The webhook absolute url. Pass <see langword="null"/> to reset the webhook.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string? WebhookUrl { get; init; }
}
