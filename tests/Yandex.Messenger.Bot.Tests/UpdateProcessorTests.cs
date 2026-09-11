namespace Yandex.Messenger.Bot.Tests;

using AutoFixture;
using Moq;
using Sdk.Abstractions;
using Sdk.Impl;
using Sdk.Models;

public class UpdateProcessorTests
{
    private readonly Update _update;
    private readonly Guid _buttonId = Guid.Parse("B27943C9-DED4-4E31-A381-DB23A7CF0813");

    public UpdateProcessorTests()
    {
        var fixture = new Fixture();
        _update = fixture.Build<Update>()
            .WithAutoProperties()
            .With(x => x.Text, "msg")
            .With(x => x.CallbackData, new ButtonData(_buttonId))
            .Create<Update>();
    }

    [Fact]
    public async Task OnNewUpdate_TwoGlobalObservers_ShouldCallTwice()
    {
        var mock = new Mock<IObserver>();
        mock.SetupGet(x => x.Message).Returns(string.Empty);

        IEnumerable<IObserver> observers =
        [
            mock.Object,
            mock.Object,
        ];
        var updateProcessor = new UpdateProcessor(observers);

        await updateProcessor.Process(_update, CancellationToken.None);

        mock.Verify(x => x.OnNewUpdate(It.IsAny<Update>(),
                It.IsAny<CancellationToken>()),
            Times.Exactly(2));
    }

    [Fact]
    public async Task OnNewUpdate_TwoGlobalTwoMsgObservers_ShouldCallFourTimes()
    {
        var mockGlobalObserver = new Mock<IObserver>();
        mockGlobalObserver.SetupGet(x => x.Message).Returns(string.Empty);

        var mockMsgObserver = new Mock<IObserver>();
        mockMsgObserver.SetupGet(x => x.Message).Returns("msg");

        IEnumerable<IObserver> observers =
        [
            mockGlobalObserver.Object,
            mockGlobalObserver.Object,
            mockMsgObserver.Object,
            mockMsgObserver.Object
        ];
        var updateProcessor = new UpdateProcessor(observers);

        await updateProcessor.Process(_update, CancellationToken.None);

        mockGlobalObserver.Verify(x => x.OnNewUpdate(It.IsAny<Update>(),
                It.IsAny<CancellationToken>()),
            Times.Exactly(2));
        mockMsgObserver.Verify(x => x.OnNewUpdate(It.IsAny<Update>(),
                It.IsAny<CancellationToken>()),
            Times.Exactly(2));
    }

    [Fact]
    public async Task OnNewUpdate_OneMsgObservers_ShouldCallOnce()
    {
        var mockMsgObserver = new Mock<IObserver>();
        mockMsgObserver.SetupGet(x => x.Message).Returns("msg");

        IEnumerable<IObserver> observers =
        [
            mockMsgObserver.Object
        ];
        var updateProcessor = new UpdateProcessor(observers);

        await updateProcessor.Process(_update, CancellationToken.None);

        mockMsgObserver.Verify(x => x.OnNewUpdate(It.IsAny<Update>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task OnNewUpdate_ButtonObservers_ShouldCallOnce()
    {
        var mockMsgObserver = new Mock<ButtonObserver>();
        mockMsgObserver.SetupGet(x => x.ButtonId).Returns(_buttonId);

        IEnumerable<IObserver> observers =
        [
            mockMsgObserver.Object
        ];
        var updateProcessor = new UpdateProcessor(observers);

        await updateProcessor.Process(_update, CancellationToken.None);

        mockMsgObserver.Verify(x => x.OnNewUpdate(It.IsAny<Update>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task OnNewUpdate_OneMsgAsGuidObserver_ShouldCallOnce()
    {
        var mockMsgObserver = new Mock<IObserver>();
        mockMsgObserver.SetupGet(x => x.Message).Returns(_buttonId.ToString());

        IEnumerable<IObserver> observers =
        [
            mockMsgObserver.Object
        ];
        var updateProcessor = new UpdateProcessor(observers);

        await updateProcessor.Process(_update, CancellationToken.None);

        mockMsgObserver.Verify(x => x.OnNewUpdate(It.IsAny<Update>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Process_WhenOneObserverThrows_RemainingObserversShouldStillBeCalled()
    {
        var throwingObserver = new Mock<IObserver>();
        throwingObserver.SetupGet(x => x.Message).Returns(string.Empty);
        throwingObserver
            .Setup(x => x.OnNewUpdate(It.IsAny<Update>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Observer failure"));

        var normalObserver1 = new Mock<IObserver>();
        normalObserver1.SetupGet(x => x.Message).Returns(string.Empty);

        var normalObserver2 = new Mock<IObserver>();
        normalObserver2.SetupGet(x => x.Message).Returns(string.Empty);

        IEnumerable<IObserver> observers =
        [
            throwingObserver.Object,
            normalObserver1.Object,
            normalObserver2.Object,
        ];
        var updateProcessor = new UpdateProcessor(observers);

        await updateProcessor.Process(_update, CancellationToken.None);

        throwingObserver.Verify(x => x.OnNewUpdate(It.IsAny<Update>(), It.IsAny<CancellationToken>()), Times.Once);
        normalObserver1.Verify(x => x.OnNewUpdate(It.IsAny<Update>(), It.IsAny<CancellationToken>()), Times.Once);
        normalObserver2.Verify(x => x.OnNewUpdate(It.IsAny<Update>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
