namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

using Models;

/// <inheritdoc />
/// <param name="Id">The bot identifier.</param>
/// <param name="Login">The bot login.</param>
/// <param name="DisplayName">The bot display name.</param>
/// <param name="WebhookUrl">The webhook URL.</param>
/// <param name="Organizations">The identifiers of organizations the bot is connected to.</param>
/// <param name="Settings">The bot event settings.</param>
public record BotInfoResponse(
    bool Ok,
    string Description,
    string Id,
    string Login,
    string? DisplayName,
    string? WebhookUrl,
    long[]? Organizations,
    BotSettings? Settings) : Response(Ok, Description);
