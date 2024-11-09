namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a poll creation request data.
/// </summary>
public record CreatePollRequest : Request
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
    /// The poll maximum choice count.
    /// </summary>
    public int MaxChoice { get; init; } = 1;

    /// <summary>
    /// Indicates that the poll is anonymous.
    /// </summary>
    public bool IsAnonymous { get; init; } = false;

    /// <summary>
    /// A payload ID.
    /// </summary>
    public string? PayloadId { get; init; }

    /// <summary>
    /// A message that the poll should be replied on.
    /// </summary>
    public int? ReplyMessageId { get; init; }

    /// <summary>
    /// Indicates that the poll should disable notifications on poll updates.
    /// </summary>
    public bool DisableNotification { get; init; } = false;

    /// <summary>
    /// Indicates that the poll is important.
    /// </summary>
    public bool Important { get; init; } = false;

    /// <summary>
    /// Indicates that in a message that represents the poll should be disabled a preview.
    /// </summary>
    public bool DisableWebPagePreview { get; init; } = false;
}