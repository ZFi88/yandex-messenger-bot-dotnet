namespace Yandex.Messenger.Bot.Sdk.Json;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Converts a UNIX timestamp value to the <see cref="DateTime"/> value.
/// </summary>
public class TimestampToDateTimeConverter : JsonConverter<DateTime>
{
    /// <inheritdoc />
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.Number)
        {
            throw new JsonException(
                $"Expected a number token for UNIX timestamp, but got {reader.TokenType}.");
        }

        return DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64()).UtcDateTime;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        var unixTime = ((DateTimeOffset)value).ToUnixTimeSeconds();
        writer.WriteNumberValue(unixTime);
    }
}