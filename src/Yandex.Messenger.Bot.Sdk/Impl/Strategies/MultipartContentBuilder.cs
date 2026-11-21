namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

using System.Net.Http;
using System.Text.Json;
using Json;

/// <summary>
/// Builds multipart form fields for Bot API requests.
/// </summary>
internal static class MultipartContentBuilder
{
    /// <summary>
    /// Adds a string field if the value is not null.
    /// </summary>
    /// <param name="content">The multipart content.</param>
    /// <param name="name">The form field name.</param>
    /// <param name="value">The form field value.</param>
    public static void AddString(MultipartFormDataContent content, string name, string? value)
    {
        if (value is not null)
        {
            content.Add(new StringContent(value), name);
        }
    }

    /// <summary>
    /// Adds a field with a JSON-serialized value if the value is not null.
    /// </summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    /// <param name="content">The multipart content.</param>
    /// <param name="name">The form field name.</param>
    /// <param name="value">The form field value.</param>
    public static void AddJson<TValue>(MultipartFormDataContent content, string name, TValue? value)
        where TValue : class
    {
        if (value is not null)
        {
            var json = JsonSerializer.Serialize(value, YandexMessengerBotJsonOptions.Value);
            content.Add(new StringContent(json), name);
        }
    }
}
