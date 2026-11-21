namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

using Models;

/// <summary>
/// Represents a bot settings update request data.
/// </summary>
public record UpdateSelfRequest
{
    /// <summary>
    /// The webhook URL. When omitted, the current webhook is not changed.
    /// </summary>
    public string? WebhookUrl { get; init; }

    /// <summary>
    /// The event settings which should be changed. The update is partial.
    /// </summary>
    public BotSettings? Settings { get; init; }
}
