namespace Yandex.Messenger.Bot.Tests;

using System.Text.Json;
using FluentAssertions;
using Messenger.Bot.Sdk.Json;

public class UnderscorePolicyTests
{
    private record TestClass(long MessageId);

    [Fact]
    public void ShouldDeserializePropertiesWithUnderscore()
    {
        var testClass = JsonSerializer.Deserialize<TestClass>("{\"message_id\":100}", new JsonSerializerOptions()
        {
            PropertyNamingPolicy = new UnderscorePolicy()
        });

        testClass!.MessageId.Should().Be(100);
    }

    [Fact]
    public void ShouldSerializePropertiesWithUnderscore()
    {
        var testClass = new TestClass(100);
        var json = JsonSerializer.Serialize(testClass, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = new UnderscorePolicy()
        });

        json.Should().BeEquivalentTo("{\"message_id\":100}");
    }
}