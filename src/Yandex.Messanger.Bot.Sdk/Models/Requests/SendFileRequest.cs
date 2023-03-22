namespace Yandex.Messanger.Bot.Sdk.Models.Requests;

public record SendFileRequest
{
    public string? ChatId { get; init; }
    public string? Login { get; init; }
    public required Stream Document { get; init; }
    
}