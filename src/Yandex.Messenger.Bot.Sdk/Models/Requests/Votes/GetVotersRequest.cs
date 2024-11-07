namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents voters getting request data.
/// </summary>
public record GetVotersRequest : Request
{
    /// <summary>
    /// A message ID of the poll.
    /// </summary>
    public required long MessageId { get; init; }

    /// <summary>
    /// Hash of invite link, if the bot is not member of the chat.
    /// </summary>
    public string? InviteHash { get; init; }

    /// <summary>
    /// The limit of voters.
    /// </summary>
    public int Limit { get; init; } = 100;

    /// <summary>
    /// The ID of vote from which should gets voters.
    /// </summary>
    public int Cursor { get; init; } = 0;

    /// <summary>
    /// The ID of the answer for getting voters.
    /// </summary>
    public required int AnswerId { get; init; }
}