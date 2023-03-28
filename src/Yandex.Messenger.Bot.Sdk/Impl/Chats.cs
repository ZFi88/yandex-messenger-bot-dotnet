namespace Yandex.Messenger.Bot.Sdk.Impl;

using Abstractions;
using Models.Requests;
using Models.Responses;
using Strategies;

internal class Chats : BaseClient, IChats
{
    public Chats(HttpClient client)
        : base(client)
    {
    }

    public async Task<CreateChatResponse> Create(CreateChatRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<CreateChatResponse>(new SendJsonStrategy("chats/create"), request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ChatUpdateResponse> UpdateChat(ChatUpdateRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<ChatUpdateResponse>(new SendJsonStrategy("chats/updateMembers"), request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<SendMessageResponse> SendMessage(SendMessageRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<SendMessageResponse>(new SendJsonStrategy("messages/sendText"), request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<SendMessageResponse> SendFile(SendFileRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<SendMessageResponse>(new SendFileStrategy(), request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<SendMessageResponse> SendImage(SendFileRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<SendMessageResponse>(new SendImageStrategy(), request, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<SendMessageResponse> SendAlbum(SendAlbumRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<SendMessageResponse>(new SendAlbumStrategy(), request, cancellationToken)
            .ConfigureAwait(false);
    }
}