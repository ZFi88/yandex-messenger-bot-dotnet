namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

/// <inheritdoc />
/// <param name="MessageId">A message ID.</param>
public record DeleteMessageResponse(bool Ok, string Description, long MessageId) : Response(Ok, Description);
