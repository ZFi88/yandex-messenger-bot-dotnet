namespace Yandex.Messanger.Bot.Sdk.Models.Requests;

public record GetUpdateRequest
{
    public int Limit { get; init; } = 100;

    public int Offset { get; init; } = 0;
}