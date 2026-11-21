namespace Yandex.Messenger.Bot.Tests;

using System.Text.Json;
using FluentAssertions;
using Sdk.Json;
using Sdk.Models;

public class UpdateDeserializationTests
{
    private static Update Deserialize(string json) =>
        JsonSerializer.Deserialize<Update>(json, YandexMessengerBotJsonOptions.Value)!;

    [Fact]
    public void ShouldDeserializePrivateTextMessage()
    {
        var update = Deserialize(
            """
            {
              "message_id": 1702323240544005,
              "timestamp": 1702323240,
              "chat": { "type": "private", "id": "chat-id" },
              "from": {
                "id": "user-guid",
                "display_name": "Ivan Ivanov",
                "login": "ivan_ivanov",
                "robot": false
              },
              "update_id": 1571239,
              "text": "Oh hi Mark"
            }
            """);

        update.Text.Should().Be("Oh hi Mark");
        update.From.Login.Should().Be("ivan_ivanov");
        update.From.Id.Should().Be("user-guid");
        update.From.DisplayName.Should().Be("Ivan Ivanov");
        update.From.Robot.Should().BeFalse();
        update.Chat.Type.Should().Be(ChatType.Private);
        update.Timestamp.Should().Be(DateTimeOffset.FromUnixTimeSeconds(1702323240).UtcDateTime);
        update.File.Should().BeNull();
    }

    [Fact]
    public void ShouldDeserializeChannelSender()
    {
        var update = Deserialize(
            """
            {
              "message_id": 1702332164147004,
              "timestamp": 1702332164,
              "chat": { "type": "channel", "id": "channel-id" },
              "from": { "id": "11111111-1111-1111-1111-111111111111" },
              "update_id": 1571269,
              "text": "Probably something important"
            }
            """);

        update.From.Login.Should().BeNull();
        update.From.Id.Should().Be("11111111-1111-1111-1111-111111111111");
        update.Chat.Type.Should().Be(ChatType.Channel);
    }

    [Fact]
    public void ShouldDeserializeMessageWithoutTextOrFile()
    {
        var update = Deserialize(
            """
            {
              "message_id": 1702328871591005,
              "timestamp": 1702328871,
              "chat": { "type": "private", "id": "chat-id" },
              "from": { "login": "ivan_ivanov" },
              "update_id": 1571247,
              "sticker": { "id": "sticker-id", "set_id": "set-id" }
            }
            """);

        update.Text.Should().BeNull();
        update.File.Should().BeNull();
        update.Sticker.Should().NotBeNull();
        update.Sticker!.Id.Should().Be("sticker-id");
        update.Sticker.SetId.Should().Be("set-id");
    }

    [Fact]
    public void ShouldDeserializeImages()
    {
        var update = Deserialize(
            """
            {
              "message_id": 1702329451492005,
              "timestamp": 1702329451,
              "chat": { "type": "private", "id": "chat-id" },
              "from": { "login": "ivan_ivanov" },
              "update_id": 1571251,
              "images": [
                [
                  { "file_id": "file-id?size=small", "width": 150, "height": 11 },
                  { "file_id": "file-id", "width": 1048, "height": 78, "size": 20362, "name": "file.jpeg" }
                ]
              ]
            }
            """);

        update.Images.Should().NotBeNull();
        update.Images!.Should().HaveCount(1);
        update.Images[0].Should().HaveCount(2);
        update.Images[0][1].FileId.Should().Be("file-id");
        update.Images[0][1].Size.Should().Be(20362);
        update.Images[0][1].Name.Should().Be("file.jpeg");
        update.Images[0][0].Size.Should().BeNull();
    }

    [Fact]
    public void ShouldDeserializeFile()
    {
        var update = Deserialize(
            """
            {
              "message_id": 1702329844441005,
              "timestamp": 1702329844,
              "chat": { "type": "private", "id": "chat-id" },
              "from": { "login": "ivan_ivanov" },
              "update_id": 1571253,
              "file": { "id": "file-id", "name": "data.txt", "size": 20 }
            }
            """);

        update.File.Should().NotBeNull();
        update.File!.Id.Should().Be("file-id");
        update.File.Name.Should().Be("data.txt");
        update.File.Size.Should().Be(20);
    }

    [Fact]
    public void ShouldDeserializeForwardedMessages()
    {
        var update = Deserialize(
            """
            {
              "message_id": 1702329071098005,
              "timestamp": 1702329071,
              "chat": { "type": "private", "id": "chat-id" },
              "from": { "login": "ivan_ivanov" },
              "update_id": 1571249,
              "forwarded_messages": [
                {
                  "message_id": 1702323240544005,
                  "timestamp": 1702323240,
                  "chat": { "type": "private", "id": "source-chat-id" },
                  "from": { "login": "petr_petrov" },
                  "text": "Something idk"
                }
              ]
            }
            """);

        update.ForwardedMessages.Should().NotBeNull();
        update.ForwardedMessages!.Should().ContainSingle();
        update.ForwardedMessages[0].Text.Should().Be("Something idk");
        update.ForwardedMessages[0].From.Login.Should().Be("petr_petrov");
    }

    [Fact]
    public void ShouldDeserializeReplyToMessage()
    {
        var update = Deserialize(
            """
            {
              "message_id": 2,
              "timestamp": 1702329071,
              "chat": { "type": "group", "id": "chat-id" },
              "from": { "login": "ivan_ivanov" },
              "update_id": 1571250,
              "text": "reply",
              "reply_to_message": {
                "message_id": 1,
                "timestamp": 1702329000,
                "chat": { "type": "group", "id": "chat-id" },
                "from": { "login": "petr_petrov" },
                "text": "original"
              }
            }
            """);

        update.ReplyToMessage.Should().NotBeNull();
        update.ReplyToMessage!.MessageId.Should().Be(1);
        update.ReplyToMessage.Text.Should().Be("original");
    }

    [Fact]
    public void ShouldDeserializeChatMembersUpdate()
    {
        var update = Deserialize(
            """
            {
              "update_id": 1571254,
              "message_id": 1702329900000005,
              "timestamp": 1702329900,
              "chat": { "type": "group", "id": "chat-id" },
              "chat_members_update": {
                "new_chat_members": [ { "login": "ivan_ivanov", "display_name": "Ivan Ivanov" } ],
                "removed_chat_members": [ { "login": "petr_petrov" } ]
              }
            }
            """);

        update.ChatMembersUpdate.Should().NotBeNull();
        update.ChatMembersUpdate!.NewChatMembers.Should().ContainSingle();
        update.ChatMembersUpdate.NewChatMembers![0].Login.Should().Be("ivan_ivanov");
        update.ChatMembersUpdate.RemovedChatMembers.Should().ContainSingle();
    }

    [Fact]
    public void ShouldDeserializeReactionEvent()
    {
        var update = Deserialize(
            """
            {
              "update_id": 1571255,
              "message_id": 1702323240544005,
              "timestamp": 1702329950,
              "chat": { "type": "group", "id": "chat-id" },
              "from": { "login": "ivan_ivanov" },
              "reaction": {
                "message_id": 1702323240544005,
                "reaction": { "type": "default_reaction", "name": "like" },
                "action": "add"
              }
            }
            """);

        update.Reaction.Should().NotBeNull();
        update.Reaction!.Action.Should().Be("add");
        update.Reaction.MessageId.Should().Be(1702323240544005);
        update.Reaction.Reaction.Name.Should().Be("like");
        update.Reaction.Reaction.Type.Should().Be("default_reaction");
    }

    [Fact]
    public void ShouldDeserializeBotRequest()
    {
        var update = Deserialize(
            """
            {
              "update_id": 1571260,
              "message_id": 1,
              "timestamp": 1702329950,
              "chat": { "type": "group", "id": "chat-id" },
              "bot_request": {
                "server_action": { "name": "like", "payload": { "value": 1 } },
                "element_id": "like-button"
              }
            }
            """);

        update.BotRequest.Should().NotBeNull();
        update.BotRequest!.ElementId.Should().Be("like-button");
        update.BotRequest.ServerAction.Should().NotBeNull();
        update.BotRequest.ServerAction!.Name.Should().Be("like");
        update.BotRequest.ServerAction.Payload.Should().NotBeNull();
        update.BotRequest.ServerAction.Payload!.Value.GetProperty("value").GetInt32().Should().Be(1);
    }
}
