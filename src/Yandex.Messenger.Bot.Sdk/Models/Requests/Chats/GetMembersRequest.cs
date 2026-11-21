namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a chat members getting request data.
/// </summary>
public record GetMembersRequest
{
    /// <summary>
    /// The target chat ID.
    /// </summary>
    public required string ChatId { get; init; }

    /// <summary>
    /// The member role filter: <c>admin</c>, <c>member</c> or <c>subscriber</c>.
    /// </summary>
    public string? Role { get; init; }

    /// <summary>
    /// The maximum number of members in the response, from 1 to 1000. The default value is 100.
    /// </summary>
    public int Limit { get; init; } = 100;

    /// <summary>
    /// The pagination offset. Use the <c>guid</c> of the last member from the previous response.
    /// </summary>
    public string? Offset { get; init; }
}
