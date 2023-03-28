namespace Yandex.Messenger.Bot.Sdk.Json;

using System.Buffers;
using System.Buffers.Text;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

public class TimestampToDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Unix timestamp is seconds past epoch
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(reader.GetInt64()).ToLocalTime();
        return dateTime;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        // The "R" standard format will always be 29 bytes.
        Span<byte> utf8Date = new byte[29];

        bool result = Utf8Formatter.TryFormat(value, utf8Date, out _, new StandardFormat('R'));
        Debug.Assert(result);

        writer.WriteStringValue(utf8Date);
    }
}