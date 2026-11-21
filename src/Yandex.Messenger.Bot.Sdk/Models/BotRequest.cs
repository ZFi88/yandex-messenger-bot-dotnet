namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a request to a bot which is delivered when a suggest button is pressed.
/// </summary>
/// <param name="ServerAction">The server action of the button.</param>
/// <param name="ElementId">The ID of the pressed element.</param>
/// <param name="Errors">The errors which occurred while executing the directives.</param>
public record BotRequest(ServerAction? ServerAction, string? ElementId, BotRequestError[]? Errors);
