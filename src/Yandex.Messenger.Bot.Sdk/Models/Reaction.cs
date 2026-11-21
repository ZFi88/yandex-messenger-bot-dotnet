namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a reaction.
/// </summary>
/// <param name="Type">The reaction type. For default reactions it is <c>default_reaction</c>.</param>
/// <param name="Name">The reaction name, for example <c>like</c> or <c>fire</c>.</param>
public record Reaction(string Type, string Name);
