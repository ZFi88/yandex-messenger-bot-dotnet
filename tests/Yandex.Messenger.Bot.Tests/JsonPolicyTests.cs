namespace Yandex.Messenger.Bot.Tests;

using System.Text.Json;
using FluentAssertions;
using Messenger.Bot.Sdk.Json;

public class JsonPolicyTests
{
    private record TestClass(long MessageId);

    [Fact]
    public void ShouldDeserialize()
    {
        var testClass = JsonSerializer.Deserialize<TestClass>("{\"message_id\":100}", new JsonSerializerOptions()
        {
            PropertyNamingPolicy = new SerializePolicy()
        });

        testClass!.MessageId.Should().Be(100);
    }

    [Fact]
    public void ShouldSerialize()
    {
        var testClass = new TestClass(100);
        var json = JsonSerializer.Serialize(testClass, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = new SerializePolicy()
        });

        json.Should().BeEquivalentTo("{\"message_id\":100}");
    }
}