namespace Yandex.Messenger.Bot.Sdk.Models;

using System.Text.Json;

/// <summary>
/// Represents a server action received when a suggest button with the
/// <c>server_action</c> directive is pressed.
/// </summary>
/// <param name="Name">The action name specified in the directive.</param>
/// <param name="Payload">The arbitrary action payload.</param>
public record ServerAction(string Name, JsonElement? Payload);
