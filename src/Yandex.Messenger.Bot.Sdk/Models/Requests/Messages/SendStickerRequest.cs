namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a sticker sending request data.
/// </summary>
public record SendStickerRequest : SendRequest
{
    /// <summary>
    /// The sticker set ID.
    /// </summary>
    public required string StickerSetId { get; init; }

    /// <summary>
    /// The sticker ID in the set.
    /// </summary>
    public required string StickerId { get; init; }

    /// <summary>
    /// A payload ID.
    /// </summary>
    public string? PayloadId { get; init; }
}
