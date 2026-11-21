namespace Yandex.Messenger.Bot.Tests;

using System.Text.Json;
using FluentAssertions;
using Sdk.Json;
using Sdk.Models.Responses;

public class ResponseDeserializationTests
{
    [Fact]
    public void CreateChatResponse_ShouldDeserializePublicChatFields()
    {
        var response = Deserialize<CreateChatResponse>(
            """
            {
              "ok": true,
              "chat_id": "chat-id",
              "invite_hash": "hash",
              "invite_link": "https://messenger.yandex.ru/join/hash"
            }
            """);

        response.ChatId.Should().Be("chat-id");
        response.InviteHash.Should().Be("hash");
        response.InviteLink.Should().Be("https://messenger.yandex.ru/join/hash");
    }

    [Fact]
    public void SendMessageResponse_ShouldDeserializeImageFields()
    {
        var response = Deserialize<SendMessageResponse>(
            """
            {
              "ok": true,
              "message_id": 42,
              "file_id": "file-id",
              "width": 400,
              "height": 300
            }
            """);

        response.MessageId.Should().Be(42);
        response.FileId.Should().Be("file-id");
        response.Width.Should().Be(400);
        response.Height.Should().Be(300);
        response.Images.Should().BeNull();
    }

    [Fact]
    public void SendMessageResponse_ShouldDeserializeAlbumImages()
    {
        var response = Deserialize<SendMessageResponse>(
            """
            {
              "ok": true,
              "message_id": 42,
              "images": [
                { "file_id": "file-id", "width": 100, "height": 50 }
              ]
            }
            """);

        response.Images.Should().ContainSingle();
        response.Images![0].FileId.Should().Be("file-id");
        response.Images[0].Width.Should().Be(100);
    }

    private static T Deserialize<T>(string json) =>
        JsonSerializer.Deserialize<T>(json, YandexMessengerBotJsonOptions.Value)!;
}
