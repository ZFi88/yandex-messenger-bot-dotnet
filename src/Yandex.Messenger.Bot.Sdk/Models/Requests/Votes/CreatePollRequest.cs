namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a poll creation request data.
/// </summary>
public record CreatePollRequest : SendRequest
{
    /// <summary>
    /// The poll title.
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// The poll answers.
    /// </summary>
    public required string[] Answers { get; init; }

    /// <summary>
    /// The poll maximum choices count.
    /// </summary>
    public int MaxChoices { get; init; } = 1;

    /// <summary>
    /// Indicates that the poll is anonymous.
    /// </summary>
    public bool IsAnonymous { get; init; } = false;

    /// <summary>
    /// A payload ID.
    /// </summary>
    public string? PayloadId { get; init; }

    /// <summary>
    /// Indicates that in a message that represents the poll should be disabled a preview.
    /// </summary>
    public bool DisableWebPagePreview { get; init; } = false;
}
