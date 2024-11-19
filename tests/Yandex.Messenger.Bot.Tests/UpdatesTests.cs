namespace Yandex.Messenger.Bot.Tests;

using Moq;
using RichardSzalay.MockHttp;
using Sdk;
using Sdk.Abstractions;
using Sdk.Extensions;
using Sdk.Impl;
using Sdk.Models;
using Sdk.Models.Requests;
using static String;

public class UpdatesTests
{
    [Fact]
    public async Task NewUpdatesShouldTriggersClassObservers()
    {
        using var botClient = CreateClient();

        var commonObserverMock = new Mock<IObserver>();
        commonObserverMock.SetupGet(x => x.Message).Returns(Empty);
        botClient.Updates.Subscribe(commonObserverMock.Object);

        var observerMock = new Mock<IObserver>();
        observerMock.SetupGet(x => x.Message).Returns("Как дела?");
        botClient.Updates.Subscribe(observerMock.Object);

        var anotherObserverMock = new Mock<IObserver>();
        anotherObserverMock.SetupGet(x => x.Message).Returns("text");
        botClient.Updates.Subscribe(anotherObserverMock.Object);

        var buttonObserverMock = new Mock<ButtonObserver>();
        buttonObserverMock.SetupGet(x => x.ButtonId).Returns(Guid.Parse("3A6EE540-7372-4D97-B172-5CF8E671D98A"));
        botClient.Updates.Subscribe(buttonObserverMock.Object);

        await botClient.Updates.GetUpdates(new GetUpdateRequest());

        observerMock.Verify(x => x.OnNewUpdate(It.IsAny<Update>(), It.IsAny<CancellationToken>()), Times.Once);
        observerMock.Verify(x => x.OnNewUpdate(It.IsAny<Update>(), It.IsAny<CancellationToken>()), Times.Once);
        anotherObserverMock.Verify(x => x.OnNewUpdate(It.IsAny<Update>(), It.IsAny<CancellationToken>()), Times.Never);
        buttonObserverMock.Verify(x => x.OnNewUpdate(It.IsAny<Update>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task NewUpdatesShouldTriggersFunctionObservers()
    {
        using var botClient = CreateClient();

        var commonObserverMock = new Mock<Func<Update, CancellationToken, Task>>();
        botClient.Updates.Subscribe(commonObserverMock.Object);

        var observerMock = new Mock<Func<Update, CancellationToken, Task>>();
        botClient.Updates.Subscribe("Как дела?", observerMock.Object);

        var anotherObserverMock = new Mock<Func<Update, CancellationToken, Task>>();
        botClient.Updates.Subscribe("text", anotherObserverMock.Object);

        var buttonObserverMock = new Mock<Func<Update, CancellationToken, Task>>();
        botClient.Updates.Subscribe(Guid.Parse("3A6EE540-7372-4D97-B172-5CF8E671D98A"), buttonObserverMock.Object);

        await botClient.Updates.GetUpdates(new GetUpdateRequest());

        observerMock.Verify(x => x.Invoke(It.IsAny<Update>(), It.IsAny<CancellationToken>()), Times.Once);
        observerMock.Verify(x => x.Invoke(It.IsAny<Update>(), It.IsAny<CancellationToken>()), Times.Once);
        anotherObserverMock.Verify(x => x.Invoke(It.IsAny<Update>(), It.IsAny<CancellationToken>()), Times.Never);
        buttonObserverMock.Verify(x => x.Invoke(It.IsAny<Update>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    private YandexMessengerBotClient CreateClient()
    {
        var mockHttp = new MockHttpMessageHandler();

        var url = $"{YandexMessengerBotClient.YandexMessengerBotApiBaseAddress}messages/getUpdates";

        mockHttp.Expect(HttpMethod.Post, url)
            .Respond("application/json",
                """
                {
                  "ok": true,
                  "updates": [
                    {
                      "seq_no": 4,
                      "from": {
                        "login": "kolya@example.org",
                        "display_name": "Nikolay",
                        "robot": false
                      },
                      "chat": {
                        "type": "private"
                      },
                      "text": "Как дела?",
                      "callback_data": {
                        "id": "3A6EE540-7372-4D97-B172-5CF8E671D98A"
                      },
                      "timestamp": 1648631900,
                      "message_id": 1648631900883004,
                      "update_id": 1569302
                    }
                  ]
                }
                """);
        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri(YandexMessengerBotClient.YandexMessengerBotApiBaseAddress);

        return new YandexMessengerBotClient(httpClient);
    }
}