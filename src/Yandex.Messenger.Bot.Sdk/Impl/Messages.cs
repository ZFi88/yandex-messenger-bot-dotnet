namespace Yandex.Messenger.Bot.Sdk.Impl;

using Abstractions;
using Models.Requests;
using Models.Responses;
using Strategies;

/// <inheritdoc cref="IMessages" />
internal class Messages : BaseClient, IMessages
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Messages"/> class.
    /// </summary>
    /// <param name="client">A http client.</param>
    public Messages(HttpClient client)
        : base(client)
    {
    }

    /// <inheritdoc/>
    public async Task<Response> SendTyping(
        SendTypingRequest request,
        CancellationToken cancellationToken = default)
    {
        return await Send<SimpleResponse>(new SendJsonStrategy("messages/sendTyping"), request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<SendMessageResponse> SendSticker(
        SendStickerRequest request,
        CancellationToken cancellationToken = default)
    {
        return await Send<SendMessageResponse>(new SendJsonStrategy("messages/sendSticker"), request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<SendMessageResponse> SendSystemMessage(
        SendSystemMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        return await Send<SendMessageResponse>(
                new SendJsonStrategy("messages/sendSystemMessage"),
                request,
                cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<PinMessageResponse> Pin(
        PinMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        return await Send<PinMessageResponse>(new SendJsonStrategy("messages/pin"), request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<UnpinMessageResponse> Unpin(
        UnpinMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        return await Send<UnpinMessageResponse>(new SendJsonStrategy("messages/unpin"), request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<Response> SendReaction(
        SendReactionRequest request,
        CancellationToken cancellationToken = default)
    {
        return await Send<SimpleResponse>(new SendJsonStrategy("messages/sendReaction"), request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<GetReactionsResponse> GetReactions(
        GetReactionsRequest request,
        CancellationToken cancellationToken = default)
    {
        return await Send<GetReactionsResponse>(
                new SendJsonStrategy("messages/getReactions"),
                request,
                cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<SendMessageResponse> ShareFile(
        ShareFileRequest request,
        CancellationToken cancellationToken = default)
    {
        return await Send<SendMessageResponse>(new SendJsonStrategy("messages/shareFile"), request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<SendMessageResponse> ShareImage(
        ShareImageRequest request,
        CancellationToken cancellationToken = default)
    {
        return await Send<SendMessageResponse>(new SendJsonStrategy("messages/shareImage"), request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<SendMessageResponse> ShareGallery(
        ShareGalleryRequest request,
        CancellationToken cancellationToken = default)
    {
        return await Send<SendMessageResponse>(
                new SendJsonStrategy("messages/shareGallery"),
                request,
                cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<Stream> GetFile(string fileId, CancellationToken cancellationToken = default)
    {
        var request = new GetFileRequest { FileId = fileId };

        return await SendForStream(
                new SendJsonToQueryStringStrategy("messages/getFile"),
                request,
                cancellationToken)
            .ConfigureAwait(false);
    }
}
