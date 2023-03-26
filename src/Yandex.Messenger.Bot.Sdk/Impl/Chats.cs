namespace Yandex.Messenger.Bot.Sdk.Impl;

using Abstractions;
using Models.Requests;
using Models.Responses;
using Strategies;

/// <inheritdoc cref="IChats"/> />
internal class Chats : BaseClient, IChats
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Chats"/> class.
    /// </summary>
    /// <param name="client">A http client.</param>
    public Chats(HttpClient client)
        : base(client)
    {
    }

    /// <inheritdoc/>
    public async Task<CreateChatResponse> Create(CreateChatRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<CreateChatResponse>(new SendJsonStrategy("chats/create"), request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<ChatUpdateResponse> UpdateChat(ChatUpdateRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<ChatUpdateResponse>(new SendJsonStrategy("chats/updateMembers"), request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<SendMessageResponse> SendMessage(SendMessageRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<SendMessageResponse>(new SendJsonStrategy("messages/sendText"), request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<SendMessageResponse> SendFile(SendFileRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<SendMessageResponse>(new SendFileStrategy(), request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<SendMessageResponse> SendImage(SendFileRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<SendMessageResponse>(new SendImageStrategy(), request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<SendMessageResponse> SendAlbum(SendAlbumRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<SendMessageResponse>(new SendAlbumStrategy(), request, cancellationToken)
            .ConfigureAwait(false);
    }
}