namespace Yandex.Messenger.Bot.Tests;

using System.Text.Json;
using FluentAssertions;
using Sdk.Json;

public class TimestampConverterTests
{
    record Data(DateTime Date);

    [Fact]
    public void DateTimeShouldBeSerializedAsTimestamp()
    {
        var testClass = new Data(new DateTime(2023, 4, 1, 0, 0, 0, DateTimeKind.Utc));
        var json = JsonSerializer.Serialize(testClass, YandexMessengerBotJsonOptions.Value);

        json.Should().BeEquivalentTo("{\"date\":1680307200}");
    }

    [Fact]
    public void TimestampsShouldBeDeserializedToDateTime()
    {
        var json = "{\"date\":1680307200}";
        var data = JsonSerializer.Deserialize<Data>(json, YandexMessengerBotJsonOptions.Value);

        data.Should().NotBeNull();
        data!.Date.Should().Be(new DateTime(2023, 4, 1));
    }
}