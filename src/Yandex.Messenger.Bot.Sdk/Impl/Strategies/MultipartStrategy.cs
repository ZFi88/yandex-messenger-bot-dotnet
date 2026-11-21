namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

using System.Globalization;
using Models.Requests;

/// <summary>
/// Generates multipart http request message.
/// </summary>
/// <typeparam name="TRequest">The type of request options and payload.</typeparam>
internal abstract class MultipartStrategy<TRequest> : BaseStrategy<TRequest>
    where TRequest : SendRequest
{
    /// <summary>
    /// Relative endpoint url.
    /// </summary>
    protected abstract string Endpoint { get; }

    /// <summary>
    /// The name of a file part of the request.
    /// </summary>
    protected abstract string FilePartName { get; }

    /// <inheritdoc/>
    protected override HttpRequestMessage CreateRequestInner(TRequest sendRequest)
    {
        var targetsCount = 0;
        targetsCount += sendRequest.ChatId is null ? 0 : 1;
        targetsCount += sendRequest.UserId is null ? 0 : 1;
        targetsCount += sendRequest.Login is null ? 0 : 1;
        if (targetsCount != 1)
        {
            throw new ArgumentException("Exactly one of ChatId, UserId or Login must be set");
        }

        var request = new HttpRequestMessage(HttpMethod.Post, Endpoint);
        request.Headers.ExpectContinue = false;
        var content = new MultipartFormDataContent();

        MultipartContentBuilder.AddString(content, "chat_id", sendRequest.ChatId);
        MultipartContentBuilder.AddString(content, "user_id", sendRequest.UserId);
        MultipartContentBuilder.AddString(content, "login", sendRequest.Login);
        MultipartContentBuilder.AddString(
            content,
            "thread_id",
            sendRequest.ThreadId?.ToString(CultureInfo.InvariantCulture));
        MultipartContentBuilder.AddString(
            content,
            "message_id",
            sendRequest.MessageId?.ToString(CultureInfo.InvariantCulture));
        MultipartContentBuilder.AddString(
            content,
            "reply_message_id",
            sendRequest.ReplyMessageId?.ToString(CultureInfo.InvariantCulture));
        MultipartContentBuilder.AddString(content, "reply_quote", sendRequest.ReplyQuote);

        if (sendRequest.DisableNotification)
        {
            MultipartContentBuilder.AddString(content, "disable_notification", "true");
        }

        if (sendRequest.Important)
        {
            MultipartContentBuilder.AddString(content, "important", "true");
        }

        MultipartContentBuilder.AddJson(content, "forwards", sendRequest.Forwards);
        MultipartContentBuilder.AddJson(content, "suggest_buttons", sendRequest.SuggestButtons);
        MultipartContentBuilder.AddJson(content, "action_buttons", sendRequest.ActionButtons);

        request.Content = content;

        return request;
    }
}
