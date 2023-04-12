using Yandex.Messenger.Bot.Sdk.Abstractions;
using Yandex.Messenger.Bot.Sdk.Models;
using Yandex.Messenger.Bot.Sdk.Models.Requests;

public class HelpObserver : IObserver
{
    private readonly IChats _chats;

    public HelpObserver(IChats chats)
    {
        _chats = chats;
    }

    public string? Message => "/help"; // for all messages use null!

    public async Task OnNewUpdate(Update update, CancellationToken cancellationToken)
    {
        await _chats.SendMessage(new SendMessageRequest()
        {
            Text = $"Hi, {update.From.Login}! I'm EchoBot. I repeat all your messages!",
            ChatId = update.Chat.Id
        }, cancellationToken);
    }
}