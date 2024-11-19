using Example.WebApp;
using Yandex.Messenger.Bot.AspNetCore.Extensions;
using Yandex.Messenger.Bot.Sdk.Abstractions;
using Yandex.Messenger.Bot.Sdk.Models.Requests;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddYandexMessengerBotSdk(builder.Configuration)
    .AddYandexMessengerObserver(async (provider, update, cancellationToken) =>
    {
        var chats = provider.GetService<IChats>();
        await chats!.SendMessage(new SendMessageRequest
        {
            Text = $"{update.From.Login} you sent: \"{update.Text}\"",
            ChatId = update.Chat.Id
        }, cancellationToken);
    })
    .AddYandexButtonObserver(
        Guid.Parse("B27943C9-DED4-4E31-A381-DB23A7CF0813"),
        async (provider, update, cancellationToken) =>
        {
            var chats = provider.GetService<IChats>();
            await chats!.SendMessage(new SendMessageRequest
            {
                Text =
                    $"{update.From.Login} you click button: \"{update.Text}\" with id: \"{update.CallbackData!.Id:D}\"",
                ChatId = update.Chat.Id
            }, cancellationToken);
        })
    .AddYandexMessengerObserver<LoggingObserver>()
    .AddYandexMessengerObserver<MyButtonObserver>();

var app = builder.Build();

app.UseYandexMessengerWebhook();

app.MapControllers();

app.Run();