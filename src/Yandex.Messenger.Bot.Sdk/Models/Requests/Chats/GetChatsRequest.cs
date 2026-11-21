namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a chats list getting request data.
/// </summary>
public record GetChatsRequest
{
    /// <summary>
    /// The maximum number of chats in the response, no more than 1000. The default value is 100.
    /// </summary>
    public int Limit { get; init; } = 100;

    /// <summary>
    /// The pagination offset. Use the <c>id</c> of the last chat from the previous response.
    /// </summary>
    public string? Offset { get; init; }
}
