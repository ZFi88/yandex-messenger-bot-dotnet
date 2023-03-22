using Yandex.Messanger.Bot.Sdk.Models.Requests;
using Yandex.Messanger.Bot.Sdk.Models.Responses;

namespace Yandex.Messanger.Bot.Sdk.Abstractions;

public interface IChats
{
    Task<CreateChatResponse> Create(CreateChatRequest request, CancellationToken cancellationToken = default);
    Task<ChatUpdateResponse> UpdateChat(ChatUpdateRequest request, CancellationToken cancellationToken = default);
    Task<SendMessageResponse> SendMessage(SendMessageRequest request, CancellationToken cancellationToken = default);
    Task<SendMessageResponse> SendFile(SendFileRequest request, CancellationToken cancellationToken = default);
    Task<SendMessageResponse> SendImage(SendFileRequest request, CancellationToken cancellationToken = default);
    Task<SendMessageResponse> SendAlbum(SendAlbumRequest request, CancellationToken cancellationToken = default);
}