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
    /// The absolute url of a webhook endpoint.
    /// </summary>
    public Uri? WebhookUrl { get; init; }
}