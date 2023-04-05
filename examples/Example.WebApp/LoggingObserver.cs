using Yandex.Messenger.Bot.Sdk.Abstractions;
using Yandex.Messenger.Bot.Sdk.Models;

public class LoggingObserver : IObserver
{
    private readonly ILogger<LoggingObserver> _logger;

    public LoggingObserver(ILogger<LoggingObserver> logger)
    {
        _logger = logger;
    }

    public string? Message => null!;

    public Task OnNewUpdate(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{update}", update);

        return Task.CompletedTask;
    }
}