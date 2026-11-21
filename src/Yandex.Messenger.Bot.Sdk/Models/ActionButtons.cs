namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents action buttons displayed under a message.
/// </summary>
/// <param name="Buttons">The action buttons. No more than six buttons are allowed.</param>
public record ActionButtons(ActionButton[] Buttons);
