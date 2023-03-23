namespace Yandex.Messanger.Bot.Tests;

using System.Text.Json;
using Sdk.Json;

public class JsonPolicyTests
{
    private record TestClass(int MessageId);

    [Fact]
    public void ShouldDeserialize()
    {
        var testClass = JsonSerializer.Deserialize<TestClass>("{\"message_id\":100}", new JsonSerializerOptions()
        {
            PropertyNamingPolicy = new SerializePolicy()
        });

        Assert.Equal(100, testClass!.MessageId);
    }

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
}