using Yandex.Messenger.Bot.Sdk;
using Yandex.Messenger.Bot.Sdk.Extensions;
using Yandex.Messenger.Bot.Sdk.Models.Requests;

var yandexBotClient = new YandexMessengerBotClient("<TOKEN>");

yandexBotClient.Updates.Subscribe((update, token) =>
{
    Console.WriteLine("from common observer");
    Console.WriteLine(update);
    return Task.CompletedTask;
});

yandexBotClient.Updates.Subscribe("/stat", async (update, token) =>
{
    Console.WriteLine("from stat observer");
    Console.WriteLine(update);
    await yandexBotClient.Chats.SendMessage(new SendMessageRequest()
    {
        Text = "привет!!!!!", Login = update.From.Login
    }, token);

});

while (true)
{
    await yandexBotClient.Updates.GetUpdates(new GetUpdateRequest());
    await Task.Delay(3000);
}