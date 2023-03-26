using Yandex.Messanger.Bot.Sdk;
using Yandex.Messanger.Bot.Sdk.Abstractions;
using Yandex.Messanger.Bot.Sdk.Extensions;
using Yandex.Messanger.Bot.Sdk.Models;
using Yandex.Messanger.Bot.Sdk.Models.Requests;

var yandexBotClient = new YandexBotClient("y0_AgAAAABpcG7nAATIlgAAAADfEDmUxFhibOUVTACs7DvIS-bz8QVZ-pI");

yandexBotClient.Updates.Subscribe(update =>
{
    Console.WriteLine("from common observer");
    Console.WriteLine(update);

    return Task.CompletedTask;
});

yandexBotClient.Updates.Subscribe("/stat", update =>
{
    Console.WriteLine("from stat observer");
    Console.WriteLine(update);

    return Task.CompletedTask;
});

while (true)
{
    await yandexBotClient.Updates.GetUpdates(new GetUpdateRequest()
    {
        Offset = 
    });

    await Task.Delay(3000);
}