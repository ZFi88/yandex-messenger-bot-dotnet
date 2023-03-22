namespace Yandex.Messanger.Bot.Sdk.Models.Requests;

public record CreateChatRequest
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public Uri? AvatarUrl { get; init; }
    public User[]? Admins { get; init; }
    public User[]? Members { get; init; }
}