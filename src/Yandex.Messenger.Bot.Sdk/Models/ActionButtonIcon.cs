namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents an icon of an action button.
/// </summary>
/// <param name="Type">The icon type. The only supported value is <c>messenger_icons</c>.</param>
/// <param name="Value">The icon name: <c>like</c>, <c>pressed_like</c>, <c>dislike</c> or <c>pressed_dislike</c>.</param>
public record ActionButtonIcon(string Type, string Value);
