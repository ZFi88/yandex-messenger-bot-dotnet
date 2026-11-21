namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

/// <summary>
/// Represents the pagination cursor of the poll voters list.
/// </summary>
/// <param name="Next">The ID of the last received vote which should be passed as the next request cursor.</param>
public record VotersCursor(long? Next);
