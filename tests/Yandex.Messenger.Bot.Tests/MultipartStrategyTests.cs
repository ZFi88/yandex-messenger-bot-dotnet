namespace Yandex.Messenger.Bot.Tests;

using System.Net.Http;
using System.Text.Json;
using FluentAssertions;
using Sdk.Impl.Strategies;
using Sdk.Models;
using Sdk.Models.Requests;

public class MultipartStrategyTests
{
    [Fact]
    public void SendFileStrategy_ShouldNotSendAnyTarget_WhenNoTargetSpecified()
    {
        var strategy = new SendFileStrategy();
        var request = new SendFileRequest
        {
            Filename = "file.txt",
            Document = new MemoryStream(),
        };

        var act = () => strategy.CreateRequest(request);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Exactly one of ChatId, UserId or Login must be set");
    }

    [Fact]
    public void SendFileStrategy_ShouldThrow_WhenSeveralTargetsSpecified()
    {
        var strategy = new SendFileStrategy();
        var request = new SendFileRequest
        {
            ChatId = "chat",
            Login = "login",
            Filename = "file.txt",
            Document = new MemoryStream(),
        };

        var act = () => strategy.CreateRequest(request);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Exactly one of ChatId, UserId or Login must be set");
    }

    [Fact]
    public async Task SendFileStrategy_ShouldIncludeCommonSendFields()
    {
        var strategy = new SendFileStrategy();
        var request = new SendFileRequest
        {
            UserId = "user-guid",
            Filename = "file.txt",
            Document = new MemoryStream(),
            ThreadId = 10,
            MessageId = 20,
            ReplyMessageId = 30,
            ReplyQuote = "quote",
            DisableNotification = true,
            Important = true,
            Forwards = [new Forward("source-chat", [1, 2])],
            SuggestButtons = new SuggestButtons
            {
                Layout = "false",
                Buttons = [[new InlineSuggestButton { Id = "b1", Title = "Button" }]],
            },
            ActionButtons = new ActionButtons(
            [
                new ActionButton
                {
                    Title = "Like",
                    Icon = new ActionButtonIcon("messenger_icons", "like"),
                    Directives = [new ServerActionDirective("like", null)],
                },
            ]),
        };

        using var message = strategy.CreateRequest(request);
        var fields = await ReadFields(message);

        fields["user_id"].Should().Be("user-guid");
        fields["thread_id"].Should().Be("10");
        fields["message_id"].Should().Be("20");
        fields["reply_message_id"].Should().Be("30");
        fields["reply_quote"].Should().Be("quote");
        fields["disable_notification"].Should().Be("true");
        fields["important"].Should().Be("true");

        using var forwards = JsonDocument.Parse(fields["forwards"]);
        forwards.RootElement[0].GetProperty("chat_id").GetString().Should().Be("source-chat");
        forwards.RootElement[0].GetProperty("message_ids")[1].GetInt64().Should().Be(2);

        using var suggestButtons = JsonDocument.Parse(fields["suggest_buttons"]);
        suggestButtons.RootElement.GetProperty("layout").GetString().Should().Be("false");
        suggestButtons.RootElement.GetProperty("buttons")[0].GetProperty("id").GetString().Should().Be("b1");

        using var actionButtons = JsonDocument.Parse(fields["action_buttons"]);
        var actionButton = actionButtons.RootElement.GetProperty("buttons")[0];
        actionButton.GetProperty("icon").GetProperty("value").GetString().Should().Be("like");
        var directive = actionButton.GetProperty("directives")[0];
        directive.GetProperty("type").GetString().Should().Be("server_action");
        directive.GetProperty("name").GetString().Should().Be("like");
    }

    [Fact]
    public async Task SendAlbumStrategy_ShouldIncludeText()
    {
        var strategy = new SendAlbumStrategy();
        var request = new SendAlbumRequest
        {
            ChatId = "chat",
            Images = new Dictionary<string, Stream> { ["photo.jpg"] = new MemoryStream() },
            Text = "caption",
        };

        using var message = strategy.CreateRequest(request);
        var fields = await ReadFields(message);

        fields["chat_id"].Should().Be("chat");
        fields["text"].Should().Be("caption");
    }

    private static async Task<Dictionary<string, string>> ReadFields(HttpRequestMessage message)
    {
        var multipart = (MultipartFormDataContent)message.Content!;
        var fields = new Dictionary<string, string>();
        foreach (var part in multipart)
        {
            var name = part.Headers.ContentDisposition?.Name?.Trim('"');
            if (name is null || name == "document" || name == "images")
            {
                continue;
            }

            fields[name] = await part.ReadAsStringAsync();
        }

        return fields;
    }
}
