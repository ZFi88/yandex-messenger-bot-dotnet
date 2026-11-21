namespace Yandex.Messenger.Bot.Sdk.Abstractions;

using Models.Requests;
using Models.Responses;

/// <summary>
/// An abstraction which represents the messages API.
/// </summary>
public interface IMessages
{
    /// <summary>
    /// Sends a typing indicator.
    /// </summary>
    /// <param name="request">The typing indicator parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<Response> SendTyping(SendTypingRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a sticker to a chat.
    /// </summary>
    /// <param name="request">The sticker sending parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<SendMessageResponse> SendSticker(SendStickerRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a system message to a chat.
    /// </summary>
    /// <param name="request">The system message sending parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<SendMessageResponse> SendSystemMessage(
        SendSystemMessageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Pins a message in a chat.
    /// </summary>
    /// <param name="request">The message pinning parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<PinMessageResponse> Pin(PinMessageRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Unpins a message in a chat.
    /// </summary>
    /// <param name="request">The message unpinning parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<UnpinMessageResponse> Unpin(UnpinMessageRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets or removes a reaction on a message.
    /// </summary>
    /// <param name="request">The reaction parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<Response> SendReaction(SendReactionRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets reactions on a message.
    /// </summary>
    /// <param name="request">The reactions getting parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<GetReactionsResponse> GetReactions(
        GetReactionsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a previously uploaded file to a chat.
    /// </summary>
    /// <param name="request">The file sharing parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<SendMessageResponse> ShareFile(ShareFileRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a previously uploaded image to a chat.
    /// </summary>
    /// <param name="request">The image sharing parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<SendMessageResponse> ShareImage(ShareImageRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends previously uploaded images as an album to a chat.
    /// </summary>
    /// <param name="request">The album sharing parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<SendMessageResponse> ShareGallery(
        ShareGalleryRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Downloads a file which was sent to a chat.
    /// </summary>
    /// <param name="fileId">The file ID.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The file content stream. The caller is responsible for disposing it.</returns>
    Task<Stream> GetFile(string fileId, CancellationToken cancellationToken = default);
}
