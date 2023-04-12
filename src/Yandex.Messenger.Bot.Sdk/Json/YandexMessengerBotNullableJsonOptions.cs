using System.Text.Json;
using System.Text.Json.Serialization;

namespace Yandex.Messenger.Bot.Sdk.Json;

/// <summary>
/// The options for the <see cref="JsonSerializer"/>.
/// </summary>
public static class YandexMessengerBotNullableJsonOptions
{
    static YandexMessengerBotNullableJsonOptions()
    {
        Value = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = new UnderscorePolicy(),
            DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        };
        Value.Converters.Add(new TimestampToDateTimeConverter());
    }

    /// <summary>
    /// The value of th options.
    /// </summary>
    public static JsonSerializerOptions Value { get; }
}