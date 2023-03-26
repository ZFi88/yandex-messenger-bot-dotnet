namespace Yandex.Messenger.Bot.Sdk.Models.Requests;

public record ChatUpdateRequest
{
    public required string ChatId { get; init; }

    public User[] Members { get; init; } = Array.Empty<User>();

    public User[] Admins { get; init; } = Array.Empty<User>();

    public User[] Subscribers { get; init; } = Array.Empty<User>();

    public User[] Remove { get; init; } = Array.Empty<User>();
}