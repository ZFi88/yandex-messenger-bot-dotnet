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
    public int Limit { get; init; } = 100;

    public long Offset { get; init; } = 0;
}