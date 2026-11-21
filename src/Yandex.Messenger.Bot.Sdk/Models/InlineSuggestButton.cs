namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a button which is displayed under a message.
/// </summary>
public record InlineSuggestButton
{
    /// <summary>
    /// The unique button identifier.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// The button text.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// The actions which are executed when the button is pressed.
    /// </summary>
    public Directive[]? Directives { get; init; }
}
