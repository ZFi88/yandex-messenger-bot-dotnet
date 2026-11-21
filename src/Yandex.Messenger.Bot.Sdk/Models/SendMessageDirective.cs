namespace Yandex.Messenger.Bot.Sdk.Models;

using System.Text.Json;

/// <summary>
/// Sends a message to the chat on behalf of the user who pressed the button.
/// </summary>
/// <param name="Text">The message text.</param>
/// <param name="Payload">The arbitrary data which the bot receives together with the message.</param>
public record SendMessageDirective(string Text, JsonElement? Payload) : Directive;
