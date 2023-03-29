namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

public record GetUpdateRequest
{
    public int Limit { get; init; } = 100;
}

internal record GetUpdateRequestInternal
{
    public int Limit { get; init; } = 100;

    public long Offset { get; init; } = 0;
}