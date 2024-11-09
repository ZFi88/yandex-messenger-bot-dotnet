namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a button.
/// </summary>
public record Button
{
    /// <summary>The button text.</summary>
    public required string Text { get; init; }

    /// <summary>An object represents a callback data..</summary>
    public ButtonData? CallbackData { get; init; }
}