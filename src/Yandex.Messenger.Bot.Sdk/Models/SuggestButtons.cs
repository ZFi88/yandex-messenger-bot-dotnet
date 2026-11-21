namespace Yandex.Messenger.Bot.Sdk.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a keyboard with buttons under a message.
/// </summary>
[JsonConverter(typeof(SuggestButtonsJsonConverter))]
public record SuggestButtons
{
    /// <summary>
    /// The buttons layout mode. <c>"false"</c> — buttons are placed in one line with wrapping,
    /// <c>"true"</c> — buttons are placed by rows according to the <see cref="Buttons"/> matrix.
    /// </summary>
    public required string Layout { get; init; }

    /// <summary>
    /// Indicates that the buttons are displayed even if the message is not the last one in the chat.
    /// </summary>
    public bool Persist { get; init; }

    /// <summary>
    /// The buttons matrix. Each nested array is a separate row.
    /// When <see cref="Layout"/> is <c>"false"</c> a single row is serialized as a flat array.
    /// </summary>
    public InlineSuggestButton[][]? Buttons { get; init; }
}
