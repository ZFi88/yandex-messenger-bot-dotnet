namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

/// <inheritdoc />
/// <param name="MessageId">A message ID.</param>
/// <param name="FileId">A file ID. Returned by the file and image sending methods.</param>
/// <param name="Width">An image width. Returned by the image sending method.</param>
/// <param name="Height">An image height. Returned by the image sending method.</param>
/// <param name="Images">Sent images. Returned by the album sending method.</param>
public record SendMessageResponse(
    bool Ok,
    string Description,
    long MessageId,
    string? FileId,
    int? Width,
    int? Height,
    Image[]? Images) : Response(Ok, Description);
