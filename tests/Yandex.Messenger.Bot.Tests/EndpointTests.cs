namespace Yandex.Messenger.Bot.Tests;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using RichardSzalay.MockHttp;
using Sdk;
using Sdk.Abstractions;
using Sdk.Models;
using Sdk.Models.Requests;

public class EndpointTests
{
    private const string DefaultResponseBody = """{"ok":true,"description":""}""";

    private const string GetUpdatesResponseBody = """{"ok":true,"description":"","updates":[]}""";

    public static IEnumerable<object[]> Data()
    {
        return new List<object[]>()
        {
            new object[]
            {
                "chats/create",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Chats.Create(new CreateChatRequest
                {
                    Name = "Name",
                    Description = "Description"
                }),
                DefaultResponseBody
            },
            new object[]
            {
                "messages/sendText",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Chats.SendMessage(new SendMessageRequest
                {
                    Text = "Text"
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "messages/delete",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Chats.DeleteMessage(new DeleteMessageRequest()
                {
                    MessageId = 100
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "messages/createPoll",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Polls.CreatePoll(new CreatePollRequest
                {
                    Title = "Title",
                    Answers = new string[]
                    {
                    }
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "messages/getUpdates",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Updates.GetUpdates(new GetUpdateRequest()),
                GetUpdatesResponseBody,
            },
            new object[]
            {
                "self/update",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Updates.SetWebhook(new SetWebhookRequest
                {
                    WebhookUrl = "WebhookUrl"
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "messages/sendFile",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Chats.SendFile(new SendFileRequest
                {
                    ChatId = "ChatId",
                    Filename = "Filename",
                    Document = new MemoryStream()
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "messages/sendImage",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Chats.SendImage(new SendFileRequest
                {
                    ChatId = "ChatId",
                    Filename = "Filename",
                    Document = new MemoryStream()
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "messages/sendGallery",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Chats.SendAlbum(new SendAlbumRequest
                {
                    ChatId = "ChatId",
                    Images = new Dictionary<string, Stream>() { { "null", new MemoryStream() } }
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "chats/updateMembers",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Chats.UpdateChat(new ChatUpdateRequest
                {
                    ChatId = "ChatId"
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "users/getUserLink",
                HttpMethod.Get,
                (IYandexMessengerBotClient x) => x.Chats.GetUserLink(new GetUserLinkRequest()
                {
                    Login = "Login"
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "chats/get",
                HttpMethod.Get,
                (IYandexMessengerBotClient x) => x.Chats.GetChats(new GetChatsRequest()),
                DefaultResponseBody,
            },
            new object[]
            {
                "chats/getChat",
                HttpMethod.Get,
                (IYandexMessengerBotClient x) => x.Chats.GetChat(new GetChatRequest { ChatId = "chat-id" }),
                DefaultResponseBody,
            },
            new object[]
            {
                "chats/getMembers",
                HttpMethod.Get,
                (IYandexMessengerBotClient x) => x.Chats.GetMembers(new GetMembersRequest { ChatId = "chat-id" }),
                DefaultResponseBody,
            },
            new object[]
            {
                "messages/sendTyping",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Messages.SendTyping(new SendTypingRequest
                {
                    ChatId = "chat-id"
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "messages/sendSticker",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Messages.SendSticker(new SendStickerRequest
                {
                    ChatId = "chat-id",
                    StickerSetId = "set-id",
                    StickerId = "sticker-id"
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "messages/sendSystemMessage",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Messages.SendSystemMessage(new SendSystemMessageRequest
                {
                    ChatId = "chat-id",
                    Text = "text"
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "messages/pin",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Messages.Pin(new PinMessageRequest
                {
                    ChatId = "chat-id",
                    MessageId = 100
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "messages/unpin",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Messages.Unpin(new UnpinMessageRequest
                {
                    ChatId = "chat-id",
                    MessageId = 100
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "messages/sendReaction",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Messages.SendReaction(new SendReactionRequest
                {
                    ChatId = "chat-id",
                    MessageId = 100,
                    Reaction = new Reaction("default_reaction", "like")
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "messages/getReactions",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Messages.GetReactions(new GetReactionsRequest
                {
                    ChatId = "chat-id",
                    MessageId = 100
                }),
                """{"ok":true,"description":"","reactions_type":"private"}""",
            },
            new object[]
            {
                "messages/shareFile",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Messages.ShareFile(new ShareFileRequest
                {
                    ChatId = "chat-id",
                    Document = new FileReference("file-id")
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "messages/shareImage",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Messages.ShareImage(new ShareImageRequest
                {
                    ChatId = "chat-id",
                    Image = new Image("file-id", 100, 100)
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "messages/shareGallery",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Messages.ShareGallery(new ShareGalleryRequest
                {
                    ChatId = "chat-id",
                    Images = [new Image("file-id", 100, 100)]
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "self/get",
                HttpMethod.Get,
                (IYandexMessengerBotClient x) => x.Bot.GetSelf(),
                """{"ok":true,"description":"","id":"bot-id","login":"bot-login"}""",
            },
            new object[]
            {
                "self/update",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Bot.UpdateSelf(new UpdateSelfRequest
                {
                    Settings = new BotSettings { GetReactions = true }
                }),
                DefaultResponseBody,
            },
        };
    }

    [Theory]
    [MemberData(nameof(Data))]
    public async Task SdkMethodsShouldCallCorrectEndpoints(
        string url,
        HttpMethod method,
        Func<IYandexMessengerBotClient, Task> action,
        string responseBody)
    {
        using var mockHttp = new MockHttpMessageHandler();

        url = $"{YandexMessengerBotClient.YandexMessengerBotApiBaseAddress}{url}";

        mockHttp.Expect(method, url).Respond("application/json", responseBody);
        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri(YandexMessengerBotClient.YandexMessengerBotApiBaseAddress);

        var botClient = new YandexMessengerBotClient(httpClient);

        await action(botClient);

        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SdkJsonToQueryStrategyShouldCallCorrectEndpoints()
    {
        using var mockHttp = new MockHttpMessageHandler();

        var url = $"{YandexMessengerBotClient.YandexMessengerBotApiBaseAddress}users/getUserLink";

        mockHttp.Expect(HttpMethod.Get, url).WithQueryString("login", "test").Respond("application/json", DefaultResponseBody);
        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri(YandexMessengerBotClient.YandexMessengerBotApiBaseAddress);

        var botClient = new YandexMessengerBotClient(httpClient);

        await botClient.Chats.GetUserLink(new GetUserLinkRequest() { Login = "test" });

        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetPollResults_ShouldPassParametersInQueryString()
    {
        using var mockHttp = new MockHttpMessageHandler();

        var url = $"{YandexMessengerBotClient.YandexMessengerBotApiBaseAddress}polls/getResults";

        mockHttp.Expect(HttpMethod.Get, url)
            .WithQueryString("message_id", "100")
            .Respond(
                "application/json",
                """{"ok":true,"description":"","answers":{"0":1},"voted_count":1}""");
        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri(YandexMessengerBotClient.YandexMessengerBotApiBaseAddress);

        var botClient = new YandexMessengerBotClient(httpClient);

        var response = await botClient.Polls.GetPollResults(new PollResultsRequest { MessageId = 100 });

        response.VotedCount.Should().Be(1);
        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetVoters_ShouldPassParametersInQueryString()
    {
        using var mockHttp = new MockHttpMessageHandler();

        var url = $"{YandexMessengerBotClient.YandexMessengerBotApiBaseAddress}polls/getVoters";

        mockHttp.Expect(HttpMethod.Get, url)
            .WithQueryString(new Dictionary<string, string>
            {
                ["message_id"] = "1000",
                ["answer_id"] = "2",
                ["limit"] = "50",
                ["cursor"] = "10",
            })
            .Respond(
                "application/json",
                """{"ok":true,"description":"","answer_id":2,"voted_count":1,"cursor":{"next":10},"votes":[]}""");
        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri(YandexMessengerBotClient.YandexMessengerBotApiBaseAddress);

        var botClient = new YandexMessengerBotClient(httpClient);

        var response = await botClient.Polls.GetVoters(new GetVotersRequest
        {
            MessageId = 1000,
            AnswerId = 2,
            Limit = 50,
            Cursor = 10,
        });

        response.AnswerId.Should().Be(2);
        response.Cursor!.Next.Should().Be(10);
        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SendTyping_ShouldAcceptEmptyResponseBody()
    {
        using var mockHttp = new MockHttpMessageHandler();

        var url = $"{YandexMessengerBotClient.YandexMessengerBotApiBaseAddress}messages/sendTyping";

        mockHttp.Expect(HttpMethod.Post, url)
            .Respond(HttpStatusCode.OK, "application/json", string.Empty);
        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri(YandexMessengerBotClient.YandexMessengerBotApiBaseAddress);

        var botClient = new YandexMessengerBotClient(httpClient);

        var response = await botClient.Messages.SendTyping(new SendTypingRequest { ChatId = "chat-id" });

        response.Ok.Should().BeTrue();
        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetChat_ShouldPassChatIdInQueryString()
    {
        using var mockHttp = new MockHttpMessageHandler();

        var url = $"{YandexMessengerBotClient.YandexMessengerBotApiBaseAddress}chats/getChat";

        mockHttp.Expect(HttpMethod.Get, url)
            .WithQueryString("chat_id", "test-chat")
            .Respond("application/json", DefaultResponseBody);
        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri(YandexMessengerBotClient.YandexMessengerBotApiBaseAddress);

        var botClient = new YandexMessengerBotClient(httpClient);

        await botClient.Chats.GetChat(new GetChatRequest { ChatId = "test-chat" });

        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetMembers_ShouldPassFiltersInQueryString()
    {
        using var mockHttp = new MockHttpMessageHandler();

        var url = $"{YandexMessengerBotClient.YandexMessengerBotApiBaseAddress}chats/getMembers";

        mockHttp.Expect(HttpMethod.Get, url)
            .WithQueryString(new Dictionary<string, string>
            {
                ["chat_id"] = "test-chat",
                ["role"] = "admin",
                ["limit"] = "50",
            })
            .Respond("application/json", DefaultResponseBody);
        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri(YandexMessengerBotClient.YandexMessengerBotApiBaseAddress);

        var botClient = new YandexMessengerBotClient(httpClient);

        await botClient.Chats.GetMembers(new GetMembersRequest
        {
            ChatId = "test-chat",
            Role = "admin",
            Limit = 50,
        });

        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task GetFile_ShouldReturnFileContentStream()
    {
        using var mockHttp = new MockHttpMessageHandler();

        var url = $"{YandexMessengerBotClient.YandexMessengerBotApiBaseAddress}messages/getFile";

        mockHttp.Expect(HttpMethod.Get, url)
            .WithQueryString("file_id", "test-file")
            .Respond("application/octet-stream", "file-content");
        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri(YandexMessengerBotClient.YandexMessengerBotApiBaseAddress);

        var botClient = new YandexMessengerBotClient(httpClient);

        using var stream = await botClient.Messages.GetFile("test-file");
        using var reader = new StreamReader(stream);
        var content = await reader.ReadToEndAsync();

        content.Should().Be("file-content");
        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public async Task SendFile_WithInvalidChatIdLoginCombination_ShouldThrowArgumentException(
        bool setChatId,
        bool setLogin)
    {
        using var mockHttp = new MockHttpMessageHandler();
        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri(YandexMessengerBotClient.YandexMessengerBotApiBaseAddress);

        var botClient = new YandexMessengerBotClient(httpClient);

        var request = new SendFileRequest
        {
            Filename = "test.txt",
            Document = new MemoryStream(),
            ChatId = setChatId ? "chatId" : null,
            Login = setLogin ? "login" : null
        };

        Func<Task> act = async () => await botClient.Chats.SendFile(request);

        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Exactly one of ChatId, UserId or Login must be set");
    }
}
