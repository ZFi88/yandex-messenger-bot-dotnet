namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a message sending request data.
/// </summary>
public record SendMessageRequest : SendRequest
{
    /// <summary>
    /// The text of the message.
    /// </summary>
    public string? Text { get; init; }

    /// <summary>
    /// A payload ID.
    /// </summary>
    public string? PayloadId { get; init; }

    /// <summary>
    /// Indicates that in the message should be disabled a preview.
    /// </summary>
    public bool DisableWebPagePreview { get; init; }

    /// <summary>
    /// An array of inline buttons which should be shown bottom of the message.
    /// </summary>
    /// <remarks>
    /// Deprecated by the API. Use <see cref="SendRequest.SuggestButtons"/> instead.
    /// The API rejects a request which contains both this property and <see cref="SendRequest.SuggestButtons"/>,
    /// so the property is omitted from the payload unless it is set explicitly.
    /// </remarks>
    public Button[]? InlineKeyboard { get; init; }
}
