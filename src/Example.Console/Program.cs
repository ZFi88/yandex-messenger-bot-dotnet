using Yandex.Messenger.Bot.Sdk;
using Yandex.Messenger.Bot.Sdk.Extensions;
using Yandex.Messenger.Bot.Sdk.Models.Requests;

var yandexBotClient = new YandexMessengerBotClient("y0_AgAAAABpcG7nAATIlgAAAADfEDmUxFhibOUVTACs7DvIS-bz8QVZ-pI");

var offset = 0L;

yandexBotClient.Updates.Subscribe(update =>
{
    Console.WriteLine("from common observer");
    Console.WriteLine(update);

    offset = update.UpdateId + 1;
    return Task.CompletedTask;
});

yandexBotClient.Updates.Subscribe("/stat", async update =>
{
    Console.WriteLine("from stat observer");
    Console.WriteLine(update);
    await yandexBotClient.Chats.SendMessage(new SendMessageRequest()
    {
        Text = "привет!!!!!", Login = update.From.Login
    });

    offset = update.UpdateId + 1;
});

while (true)
{
    await yandexBotClient.Updates.GetUpdates(new GetUpdateRequest()
    {
        Offset = offset
    });
    await Task.Delay(3000);
}