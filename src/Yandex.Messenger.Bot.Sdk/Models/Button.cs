namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a button.
/// </summary>
/// <param name="Text">The button text.</param>
/// <param name="Json">An object represents a callback data..</param>
/// <typeparam name="TCallbackData">The callback data type.</typeparam>
public record Button<TCallbackData>(string Text, TCallbackData Json);