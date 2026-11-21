namespace Yandex.Messenger.Bot.Sdk.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Represents an action which is executed when a suggest button is pressed.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(OpenUriDirective), "open_uri")]
[JsonDerivedType(typeof(SendMessageDirective), "send_message")]
[JsonDerivedType(typeof(ServerActionDirective), "server_action")]
[JsonDerivedType(typeof(SetElementsStateDirective), "set_elements_state")]
public abstract record Directive;
