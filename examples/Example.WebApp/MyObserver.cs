using Yandex.Messenger.Bot.Sdk.Abstractions;
using Yandex.Messenger.Bot.Sdk.Models;

public class MyObserver : IObserver
{
    private readonly ILogger<MyObserver> _logger;
    private readonly IChats _chats;

    public MyObserver(ILogger<MyObserver> logger, IChats chats)
    {
        _logger = logger;
        _chats = chats;
    }

    public string? Message => "text";

    public Task OnNewUpdate(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{update}", update);

        return Task.CompletedTask;
    }
}