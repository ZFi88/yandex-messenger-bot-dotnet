using Yandex.Messenger.Bot.AspNetCore.Extensions;
using Yandex.Messenger.Bot.Sdk.Abstractions;
using Yandex.Messenger.Bot.Sdk.Models.Requests;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddYandexMessengerBotSdk(builder.Configuration);
builder.Services.AddYandexMessengerObserver(async (provider, update, cancellationToken) =>
{
    var chats = provider.GetService<IChats>();
    await chats!.SendMessage(new SendMessageRequest
    {
        Text = $"{update.From.Login} you sent: \"{update.Text}\"",
        ChatId = update.Chat.Id
    }, cancellationToken);
});

builder.Services.AddYandexMessengerObserver<LoggingObserver>();

var app = builder.Build();

app.UseYandexMessengerWebhook();

app.MapControllers();

app.Run();