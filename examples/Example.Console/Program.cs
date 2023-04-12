using Yandex.Messenger.Bot.Sdk;
using Yandex.Messenger.Bot.Sdk.Extensions;
using Yandex.Messenger.Bot.Sdk.Models.Requests;

var yandexBotClient = new YandexMessengerBotClient("<TOKEN>");

yandexBotClient.Updates.Subscribe(async (update, cancel) =>
{
    await yandexBotClient.Chats.SendMessage(new SendMessageRequest
    {
        Text = $"{update.From.Login} you sent: \"{update.Text}\"",
        ChatId = update.Chat.Id
    }, cancel);
});

yandexBotClient.Updates.Subscribe("/help", async (update, token) =>
{
    await yandexBotClient.Chats.SendMessage(new SendMessageRequest()
    {
        Text = $"Hi, {update.From.Login}! I'm EchoBot. I repeat all your messages!",
        ChatId = update.Chat.Id
    }, token);
});

yandexBotClient.Updates.Subscribe(new HelpObserver(yandexBotClient.Chats));

while (true)
{
    await yandexBotClient.Updates.GetUpdates(new GetUpdateRequest());
    await Task.Delay(3000);
}