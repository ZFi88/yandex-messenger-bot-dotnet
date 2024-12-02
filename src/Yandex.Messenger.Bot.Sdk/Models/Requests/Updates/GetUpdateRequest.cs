namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents an updates getting request data.
/// </summary>
public record GetUpdateRequest
{
    /// <summary>
    /// The limit of updates.
    /// </summary>
    public int Limit { get; init; } = 100;
}

#pragma warning disable SA1402,SA1600 // File may only contain a single type, Elements must be documented
internal record GetUpdateRequestInternal
{
    /// <summary>
    ///  The limit of updates.
    /// </summary>
    public int Limit { get; init; } = 100;

    /// <summary>
    /// The offset of updates.
    /// </summary>
    public long Offset { get; init; } = 0;
}