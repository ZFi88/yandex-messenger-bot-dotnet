namespace Example.WebApp;

using Yandex.Messenger.Bot.Sdk.Abstractions;
using Yandex.Messenger.Bot.Sdk.Models;

public class LoggingObserver(ILogger<LoggingObserver> logger) : IObserver
{
    public string? Message => null!;

    public Task OnNewUpdate(Update update, CancellationToken cancellationToken)
    {
        logger.LogInformation("{update}", update);

        return Task.CompletedTask;
    }
}