namespace Yandex.Messenger.Bot.Tests;

using System.Net;
using RichardSzalay.MockHttp;
using Sdk;
using Sdk.Abstractions;
using Sdk.Models.Requests;

public class EndpointTests
{
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
                })
            },
            new object[]
            {
                "messages/sendText",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Chats.SendMessage(new SendMessageRequest
                {
                    Text = "Text"
                }),
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
            },
            new object[]
            {
                "polls/getResults",
                HttpMethod.Get,
                (IYandexMessengerBotClient x) => x.Polls.GetPollResults(new PollResultsRequest
                {
                    MessageId = 100,
                }),
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
            },
            new object[]
            {
                "messages/getUpdates",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Updates.GetUpdates(new GetUpdateRequest()),
            },
            new object[]
            {
                "self/update",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Updates.SetWebhook(new SetWebhookRequest
                {
                    WebhookUrl = "WebhookUrl"
                }),
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
            },
            new object[]
            {
                "chats/updateMembers",
                HttpMethod.Post,
                (IYandexMessengerBotClient x) => x.Chats.UpdateChat(new ChatUpdateRequest
                {
                    ChatId = "ChatId"
                }),
            },
            new object[]
            {
                "users/getUserLink",
                HttpMethod.Get,
                (IYandexMessengerBotClient x) => x.Chats.GetUserLink(new GetUserLinkRequest()
                {
                    Login = "Login"
                }),
            },
        };
    }

    [Theory]
    [MemberData(nameof(Data))]
    public async Task SdkMethodsShouldCallCorrectEndpoints(
        string url,
        HttpMethod method,
        Func<IYandexMessengerBotClient, Task> action)
    {
        using var mockHttp = new MockHttpMessageHandler();

        url = $"{YandexMessengerBotClient.YandexMessengerBotApiBaseAddress}{url}";

        mockHttp.Expect(method, url).Respond(HttpStatusCode.OK);
        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri(YandexMessengerBotClient.YandexMessengerBotApiBaseAddress);

        var botClient = new YandexMessengerBotClient(httpClient);

        try
        {
            await action(botClient);
        }
        catch
        {
            // ignore
        }

        mockHttp.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task SdkJsonToQueryStrategyShouldCallCorrectEndpoints()
    {
        using var mockHttp = new MockHttpMessageHandler();

        var url = $"{YandexMessengerBotClient.YandexMessengerBotApiBaseAddress}users/getUserLink";

        mockHttp.Expect(HttpMethod.Get, url).WithQueryString("login", "test").Respond(HttpStatusCode.OK);
        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri(YandexMessengerBotClient.YandexMessengerBotApiBaseAddress);

        var botClient = new YandexMessengerBotClient(httpClient);

        try
        {
            await botClient.Chats.GetUserLink(new GetUserLinkRequest() { Login = "test" });
        }
        catch
        {
            // ignore
        }

        mockHttp.VerifyNoOutstandingExpectation();
    }
}