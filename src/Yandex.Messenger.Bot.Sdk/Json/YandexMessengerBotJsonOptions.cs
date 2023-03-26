namespace Yandex.Messenger.Bot.Sdk.Json;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// The options for the <see cref="JsonSerializer"/>.
/// </summary>
public static class YandexMessengerBotJsonOptions
{
    static YandexMessengerBotJsonOptions()
    {
        Value = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true, PropertyNamingPolicy = new UnderscorePolicy(),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        };
        Value.Converters.Add(new TimestampToDateTimeConverter());
    }

    /// <summary>
    /// The value of th options.
    /// </summary>
    public static JsonSerializerOptions Value { get; }
}