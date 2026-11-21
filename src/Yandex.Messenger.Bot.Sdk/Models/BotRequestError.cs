namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents an error which occurred while a directive was executed on the client.
/// </summary>
/// <param name="Type">The error type.</param>
/// <param name="Name">The name of the directive which caused the error.</param>
/// <param name="Message">The error message.</param>
public record BotRequestError(string Type, string? Name, string? Message);
