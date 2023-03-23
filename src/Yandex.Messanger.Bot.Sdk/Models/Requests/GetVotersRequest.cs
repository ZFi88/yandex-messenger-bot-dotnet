namespace Yandex.Messanger.Bot.Sdk.Models.Requests;

public record GetVotersRequest
{
    public string? ChatId { get; init; }

    public string? Login { get; init; }

    public required int MessageId { get; init; }

    public string? InviteHash { get; init; }

    public int Limit { get; init; } = 100;

    public int Cursor { get; init; } = 0;

    public required int AnswerId { get; init; }
}