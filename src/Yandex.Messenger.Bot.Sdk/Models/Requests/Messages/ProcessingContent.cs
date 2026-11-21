namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents the processing indicator parameters.
/// </summary>
/// <param name="Display">The display mode: <c>text</c> or <c>default</c>.</param>
/// <param name="Text">The indicator text. Required when <paramref name="Display"/> is <c>text</c>.</param>
public record ProcessingContent(string Display, string? Text);
