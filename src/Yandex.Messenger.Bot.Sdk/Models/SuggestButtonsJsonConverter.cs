namespace Yandex.Messenger.Bot.Sdk.Models;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Converts <see cref="SuggestButtons"/> supporting both flat and matrix button layouts.
/// </summary>
internal sealed class SuggestButtonsJsonConverter : JsonConverter<SuggestButtons>
{
    /// <inheritdoc/>
    public override SuggestButtons Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException($"Expected an object token, but got {reader.TokenType}.");
        }

        string? layout = null;
        var persist = false;
        InlineSuggestButton[][]? buttons = null;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException($"Unexpected token {reader.TokenType}.");
            }

            var propertyName = reader.GetString();
            reader.Read();

            switch (propertyName)
            {
                case "layout":
                    layout = reader.GetString();
                    break;
                case "persist":
                    persist = reader.GetBoolean();
                    break;
                case "buttons":
                    buttons = ReadButtons(ref reader, options);
                    break;
                default:
                    reader.Skip();
                    break;
            }
        }

        return new SuggestButtons
        {
            Layout = layout ?? "false",
            Persist = persist,
            Buttons = buttons,
        };
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, SuggestButtons value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("layout", value.Layout);

        if (value.Persist)
        {
            writer.WriteBoolean("persist", true);
        }

        if (value.Buttons is not null)
        {
            writer.WritePropertyName("buttons");

            var isFlat = string.Equals(value.Layout, "false", StringComparison.OrdinalIgnoreCase)
                         && value.Buttons.Length == 1;
            if (isFlat)
            {
                JsonSerializer.Serialize(writer, value.Buttons[0], options);
            }
            else
            {
                JsonSerializer.Serialize(writer, value.Buttons, options);
            }
        }

        writer.WriteEndObject();
    }

    private static InlineSuggestButton[][]? ReadButtons(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        var root = document.RootElement;

        if (root.ValueKind != JsonValueKind.Array)
        {
            return null;
        }

        if (root.GetArrayLength() == 0)
        {
            return Array.Empty<InlineSuggestButton[]>();
        }

        return root[0].ValueKind == JsonValueKind.Array
            ? root.Deserialize<InlineSuggestButton[][]>(options)
            : new[] { root.Deserialize<InlineSuggestButton[]>(options)! };
    }
}
