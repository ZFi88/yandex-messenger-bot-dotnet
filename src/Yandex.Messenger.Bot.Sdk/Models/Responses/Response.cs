namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

/// <summary>
/// Represents the API response.
/// </summary>
/// <param name="Ok">Indicates that the response is success.</param>
/// <param name="Description">A description of the response.</param>
public record Response(bool Ok, string Description);