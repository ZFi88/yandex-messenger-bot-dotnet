namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

/// <inheritdoc />
/// <param name="MessageId">A message ID.</param>
public record SendMessageResponse(bool Ok, string Description, long MessageId) : Response(Ok, Description);