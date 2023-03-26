namespace Yandex.Messenger.Bot.AspNetCore.Options;

public class YandexMessengerBotOptions
{
    public const string SectionName = "YandexMessengerBot";

    public string? Token { get; init; }

    public Uri? WebhookUrl { get; init; }
}