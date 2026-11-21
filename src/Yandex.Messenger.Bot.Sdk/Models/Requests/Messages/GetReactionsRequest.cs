namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a reactions getting request data.
/// </summary>
public record GetReactionsRequest : Request
{
    /// <summary>
    /// The ID of the message which reactions should be get.
    /// </summary>
    public required long MessageId { get; init; }

    /// <summary>
    /// The maximum number of reactions in the response, from 1 to 1000. The default value is 100.
    /// </summary>
    public int Limit { get; init; } = 100;

    /// <summary>
    /// The ID of the last received reaction used for pagination.
    /// </summary>
    public long Offset { get; init; }
}
