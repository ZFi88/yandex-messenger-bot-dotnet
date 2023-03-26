namespace Yandex.Messenger.Bot.Sdk.Abstractions;

using Models.Requests;
using Models.Responses;

public interface IChats
{
    Task<CreateChatResponse> Create(CreateChatRequest request, CancellationToken cancellationToken = default);
    Task<ChatUpdateResponse> UpdateChat(ChatUpdateRequest request, CancellationToken cancellationToken = default);
    Task<SendMessageResponse> SendMessage(SendMessageRequest request, CancellationToken cancellationToken = default);
    Task<SendMessageResponse> SendFile(SendFileRequest request, CancellationToken cancellationToken = default);
    Task<SendMessageResponse> SendImage(SendFileRequest request, CancellationToken cancellationToken = default);
    Task<SendMessageResponse> SendAlbum(SendAlbumRequest request, CancellationToken cancellationToken = default);
}