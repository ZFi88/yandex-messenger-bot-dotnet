namespace Example.WebApp;

using Yandex.Messenger.Bot.Sdk.Impl;
using Yandex.Messenger.Bot.Sdk.Models;

public class MyButtonObserver(ILogger<LoggingObserver> logger) : ButtonObserver
{
    public override Guid? ButtonId
    {
        get => Guid.Parse("B27943C9-DED4-4E31-A381-DB23A7CF0813");
        protected init { }
    }

    public override Task OnNewUpdate(Update update, CancellationToken cancellationToken)
    {
        logger.LogInformation("{buttonId}", update.CallbackData!.Id);

        return Task.CompletedTask;
    }
}