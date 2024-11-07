namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a message with inline keyboard sending request data.
/// </summary>
/// <typeparam name="TCallbackData">The callback data type.</typeparam>
public record SendMessageWithKeyboardRequest<TCallbackData> : SendMessageRequest
{
    /// <summary>
    /// An array of inline buttons which should be shown bottom of the message.
    /// </summary>
    public Button<TCallbackData>[] InlineKeyboard { get; init; } = Array.Empty<Button<TCallbackData>>();
}