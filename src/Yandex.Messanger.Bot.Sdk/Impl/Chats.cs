namespace Yandex.Messanger.Bot.Sdk.Impl;

using Abstractions;
using Models.Requests;
using Models.Responses;

internal class Chats : BaseClient, IChats
{
    public Chats(HttpClient client)
        : base(client)
    {
    }

    public async Task<CreateChatResponse> Create(CreateChatRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<CreateChatResponse>("chats/create", HttpMethod.Post, request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ChatUpdateResponse> UpdateChat(ChatUpdateRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<ChatUpdateResponse>("chats/updateMembers", HttpMethod.Post, request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<SendMessageResponse> SendMessage(SendMessageRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<SendMessageResponse>("messages/sendText", HttpMethod.Post, request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<SendMessageResponse> SendFile(SendFileRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<SendMessageResponse>("messages/sendFile", HttpMethod.Post, request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<SendMessageResponse> SendImage(SendFileRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<SendMessageResponse>("messages/sendImage", HttpMethod.Post, request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<SendMessageResponse> SendAlbum(SendAlbumRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<SendMessageResponse>("messages/sendGallery", HttpMethod.Post, request, cancellationToken)
            .ConfigureAwait(false);
    }
}