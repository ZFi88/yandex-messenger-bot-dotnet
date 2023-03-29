namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

public record GetUpdateRequest
{
    public int Limit { get; init; } = 100;

    internal long Offset { get; init; } = 0;
}