namespace Yandex.Messenger.Bot.Tests;

using System.Text.Json;
using FluentAssertions;
using Sdk.Json;
using Sdk.Models;
using Sdk.Models.Requests;

public class RequestSerializationTests
{
    [Fact]
    public void CreatePollRequest_ShouldUseMaxChoicesProperty()
    {
        var request = new CreatePollRequest
        {
            Title = "Title",
            Answers = ["one", "two"],
            MaxChoices = 2,
        };

        using var document = Serialize(request);

        document.RootElement.TryGetProperty("max_choices", out var maxChoices).Should().BeTrue();
        maxChoices.GetInt32().Should().Be(2);
        document.RootElement.TryGetProperty("max_choice", out _).Should().BeFalse();
    }

    [Fact]
    public void SendMessageRequest_ShouldSerializeReplyMessageIdAsNumber()
    {
        var request = new SendMessageRequest
        {
            Text = "text",
            ReplyMessageId = 42,
        };

        using var document = Serialize(request);

        document.RootElement.GetProperty("reply_message_id").ValueKind.Should().Be(JsonValueKind.Number);
    }

    [Fact]
    public void SendMessageRequest_ShouldOmitDefaultFlags()
    {
        var request = new SendMessageRequest
        {
            Text = "text",
        };

        using var document = Serialize(request);

        document.RootElement.TryGetProperty("disable_notification", out _).Should().BeFalse();
        document.RootElement.TryGetProperty("important", out _).Should().BeFalse();
        document.RootElement.TryGetProperty("disable_web_page_preview", out _).Should().BeFalse();
        document.RootElement.TryGetProperty("reply_message_id", out _).Should().BeFalse();
    }

    [Fact]
    public void User_ShouldSerializeOnlyLogin()
    {
        using var document = Serialize(new User("ivan_ivanov"));

        document.RootElement.EnumerateObject().Should().ContainSingle();
        document.RootElement.GetProperty("login").GetString().Should().Be("ivan_ivanov");
    }

    [Fact]
    public void SendMessageRequest_ShouldSerializeForwardsAndButtons()
    {
        var request = new SendMessageRequest
        {
            Text = "text",
            Forwards = [new Forward("source-chat", [1, 2])],
            SuggestButtons = new SuggestButtons
            {
                Layout = "true",
                Persist = true,
                Buttons =
                [
                    [
                        new InlineSuggestButton
                        {
                            Id = "button-1",
                            Title = "Open",
                            Directives = [new OpenUriDirective("https://example.org")],
                        },
                        new InlineSuggestButton
                        {
                            Title = "Send",
                            Directives = [new SendMessageDirective("hello", null)],
                        },
                    ],
                ],
            },
            ActionButtons = new ActionButtons(
            [
                new ActionButton
                {
                    Id = "like",
                    Title = "Like",
                    Icon = new ActionButtonIcon("messenger_icons", "like"),
                    Directives = [new SetElementsStateDirective(["like"], "disabled", 10)],
                },
            ]),
        };

        using var document = Serialize(request);

        var forwards = document.RootElement.GetProperty("forwards");
        forwards[0].GetProperty("chat_id").GetString().Should().Be("source-chat");
        forwards[0].GetProperty("message_ids")[0].GetInt64().Should().Be(1);

        var suggestButtons = document.RootElement.GetProperty("suggest_buttons");
        suggestButtons.GetProperty("layout").GetString().Should().Be("true");
        suggestButtons.GetProperty("persist").GetBoolean().Should().BeTrue();

        var buttons = suggestButtons.GetProperty("buttons");
        buttons.GetArrayLength().Should().Be(1);
        var firstButton = buttons[0][0];
        firstButton.GetProperty("id").GetString().Should().Be("button-1");
        var openUri = firstButton.GetProperty("directives")[0];
        openUri.GetProperty("type").GetString().Should().Be("open_uri");
        openUri.GetProperty("uri").GetString().Should().Be("https://example.org");

        var sendMessage = buttons[0][1].GetProperty("directives")[0];
        sendMessage.GetProperty("type").GetString().Should().Be("send_message");
        sendMessage.GetProperty("text").GetString().Should().Be("hello");

        var actionButton = document.RootElement.GetProperty("action_buttons").GetProperty("buttons")[0];
        actionButton.GetProperty("id").GetString().Should().Be("like");
        var setState = actionButton.GetProperty("directives")[0];
        setState.GetProperty("type").GetString().Should().Be("set_elements_state");
        setState.GetProperty("state").GetString().Should().Be("disabled");
        setState.GetProperty("timeout_seconds").GetInt32().Should().Be(10);
    }

    [Fact]
    public void SendMessageRequest_WithText_SuggestButtonsShouldBeFlat_WhenLayoutIsFalse()
    {
        var request = new SendMessageRequest
        {
            Text = "text",
            SuggestButtons = new SuggestButtons
            {
                Layout = "false",
                Buttons =
                [
                    [
                        new InlineSuggestButton { Title = "One" },
                        new InlineSuggestButton { Title = "Two" },
                    ],
                ],
            },
        };

        using var document = Serialize(request);

        var buttons = document.RootElement.GetProperty("suggest_buttons").GetProperty("buttons");
        buttons.ValueKind.Should().Be(JsonValueKind.Array);
        buttons.GetArrayLength().Should().Be(2);
        buttons[0].GetProperty("title").GetString().Should().Be("One");
    }

    [Fact]
    public void CreateChatRequest_ShouldSerializePublicFlag()
    {
        var request = new CreateChatRequest
        {
            Name = "Name",
            Description = "Description",
            Public = true,
        };

        using var document = Serialize(request);

        document.RootElement.GetProperty("public").GetBoolean().Should().BeTrue();
    }

    [Fact]
    public void ChatUpdateRequest_ShouldSerializeMembersUpdatedNotificationsFlag()
    {
        var request = new ChatUpdateRequest
        {
            ChatId = "chat",
            SendMembersUpdatedNotifications = true,
        };

        using var document = Serialize(request);

        document.RootElement.GetProperty("send_members_updated_notifications").GetBoolean().Should().BeTrue();
    }

    [Fact]
    public void SetWebhookRequest_ShouldSerializeNullWebhookUrlToResetIt()
    {
        var request = new SetWebhookRequest
        {
            WebhookUrl = null,
        };

        using var document = Serialize(request);

        document.RootElement.TryGetProperty("webhook_url", out var webhookUrl).Should().BeTrue();
        webhookUrl.ValueKind.Should().Be(JsonValueKind.Null);
    }

    [Fact]
    public void UpdateSelfRequest_ShouldSerializeOnlySpecifiedSettings()
    {
        var request = new UpdateSelfRequest
        {
            Settings = new BotSettings { GetReactions = true },
        };

        using var document = Serialize(request);

        document.RootElement.TryGetProperty("webhook_url", out _).Should().BeFalse();
        var settings = document.RootElement.GetProperty("settings");
        settings.GetProperty("get_reactions").GetBoolean().Should().BeTrue();
        settings.TryGetProperty("get_members_changed", out _).Should().BeFalse();
    }

    private static JsonDocument Serialize<T>(T value) =>
        JsonDocument.Parse(JsonSerializer.Serialize(value, YandexMessengerBotJsonOptions.Value));
}
