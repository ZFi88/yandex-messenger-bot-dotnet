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