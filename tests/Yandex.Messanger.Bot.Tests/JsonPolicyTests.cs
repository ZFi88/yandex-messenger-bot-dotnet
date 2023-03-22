using System.Text.Json;
using Yandex.Messanger.Bot.Sdk.Json;

namespace Yandex.Messanger.Bot.Tests;

public class JsonPolicyTests
{
    [Fact]
    public void ShouldSerialize()
    {
        var testClass = new TestClass(100);
        var json = JsonSerializer.Serialize(testClass, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = new SerializePolicy()
        });

        Assert.Equal("{\"message_id\":100}", json);
    }

    [Fact]
    public void ShouldDeserialize()
    {
        var testClass = JsonSerializer.Deserialize<TestClass>("{\"message_id\":100}", new JsonSerializerOptions()
        {
            PropertyNamingPolicy = new SerializePolicy()
        });

        Assert.Equal(100, testClass!.MessageId);
    }

    record TestClass(int MessageId);
}