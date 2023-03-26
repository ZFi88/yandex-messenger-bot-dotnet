namespace Yandex.Messenger.Bot.Sdk.Json;

using System.Text.Json;
using System.Text.Json.Serialization;

public static class YandexMessengerBotJsonOptions
{
    static YandexMessengerBotJsonOptions()
    {
        Value = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true, PropertyNamingPolicy = new SerializePolicy(),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };
        Value.Converters.Add(new TimestampToDateTimeConverter());
    }

    public static JsonSerializerOptions Value { get; }
}