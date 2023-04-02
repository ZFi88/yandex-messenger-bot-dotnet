using Yandex.Messenger.Bot.AspNetCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddYandexMessengerBotSdk(builder.Configuration);
builder.Services.AddYandexMessengerObserver((provider, update, cancellationToken) =>
{
    var logger = provider.GetService<ILogger<Program>>();
    logger!.LogInformation("common observer", update);

    return Task.CompletedTask;
});

builder.Services.AddYandexMessengerObserver<MyObserver>();

var app = builder.Build();

app.UseYandexMessengerWebhook();

app.MapControllers();

app.Run();