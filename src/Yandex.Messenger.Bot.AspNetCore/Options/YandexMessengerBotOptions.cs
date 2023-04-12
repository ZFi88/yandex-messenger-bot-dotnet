namespace Yandex.Messenger.Bot.AspNetCore.Options;

/// <summary>
/// The Yandex Messenger Bot options.
/// </summary>
public class YandexMessengerBotOptions
{
    /// <summary>
    /// The section name of the ASPNET Core configuration.
    /// </summary>
    public const string SectionName = "YandexMessengerBot";

    /// <summary>
    /// The access token. Provides an administrator of the bot.
    /// </summary>
    public string? Token { get; init; }

    /// <summary>
    /// The webhook endpoint.
    /// </summary>
    public string WebhookEndpoint { get; init; } = "/hook";
}