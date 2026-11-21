namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

/// <summary>
/// Represents a typing indicator sending request data.
/// </summary>
public record SendTypingRequest : Request
{
    /// <summary>
    /// The indicator type: <c>text</c> or <c>processing</c>. The default value is <c>text</c>.
    /// </summary>
    public string? Type { get; init; }

    /// <summary>
    /// The indicator display time in seconds, from 1 to 60. The default value is 3.
    /// </summary>
    public int? Timeout { get; init; }

    /// <summary>
    /// The processing indicator parameters. Required when <see cref="Type"/> is <c>processing</c>.
    /// </summary>
    public ProcessingContent? ProcessingContent { get; init; }
}
