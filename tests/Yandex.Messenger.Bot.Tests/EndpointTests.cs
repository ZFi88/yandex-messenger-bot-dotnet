namespace Yandex.Messenger.Bot.Tests;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using RichardSzalay.MockHttp;
using Sdk;
using Sdk.Abstractions;
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
                "polls/getResults",
                HttpMethod.Get,
                (IYandexMessengerBotClient x) => x.Polls.GetPollResults(new PollResultsRequest
                {
                    MessageId = 100,
                }),
                DefaultResponseBody,
            },
            new object[]
            {
                "polls/getVoters",
                HttpMethod.Get,
                (IYandexMessengerBotClient x) => x.Polls.GetVoters(new GetVotersRequest
                {
                    MessageId = 1000,
                    AnswerId = 100
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

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Either ChatId or Login must be set");
    }
}
