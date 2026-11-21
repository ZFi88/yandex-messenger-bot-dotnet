namespace Yandex.Messenger.Bot.Sdk.Models;

using System.Text.Json;

/// <summary>
/// Sends a server action to the bot. The message does not appear in the chat.
/// </summary>
/// <param name="Name">The action name.</param>
/// <param name="Payload">The arbitrary action data.</param>
public record ServerActionDirective(string Name, JsonElement? Payload) : Directive;
