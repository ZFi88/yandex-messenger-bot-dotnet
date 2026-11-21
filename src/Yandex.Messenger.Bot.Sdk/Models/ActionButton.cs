namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents an action button displayed under a message.
/// </summary>
public record ActionButton
{
    /// <summary>
    /// The button identifier.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// The button text.
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// The button icon.
    /// </summary>
    public required ActionButtonIcon Icon { get; init; }

    /// <summary>
    /// The actions which are executed when the button is pressed.
    /// </summary>
    public required Directive[] Directives { get; init; }
}
