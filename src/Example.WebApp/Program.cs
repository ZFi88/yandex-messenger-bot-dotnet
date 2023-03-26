using Yandex.Messenger.Bot.AspNetCore.Extensions;
using Yandex.Messenger.Bot.Sdk.Abstractions;
using Yandex.Messenger.Bot.Sdk.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddYandexMessengerBotSdk(builder.Configuration);
builder.Services.AddYandexMessengerObserver((provider, update) =>
{
    var logger = provider.GetService<ILogger<Program>>();
    logger.LogInformation("common observer", update);

    return Task.CompletedTask;
});

builder.Services.AddYandexMessengerObserver<MyObserver>();

var app = builder.Build();

app.UseYandexMessangerWebhook();

app.MapControllers();

app.Run();

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

    public Task OnNewUpdate(Update update)
    {
        _logger.LogInformation("{update}", update);

        return Task.CompletedTask;
    }
}