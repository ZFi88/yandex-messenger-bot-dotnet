namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

public abstract record FileRequest
{
    public string? ChatId { get; init; }

    public string? Login { get; init; }
}