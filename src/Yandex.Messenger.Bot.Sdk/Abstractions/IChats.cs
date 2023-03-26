namespace Yandex.Messenger.Bot.Sdk.Abstractions;

using Models.Requests;
using Models.Responses;

/// <summary>
/// An abstraction which represents the chats API.
/// </summary>
public interface IChats
{
    /// <summary>
    /// Creates a new chat.
    /// </summary>
    /// <param name="request">The new chat creation request parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<CreateChatResponse> Create(CreateChatRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a chat.
    /// </summary>
    /// <param name="request">The chat update request parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<ChatUpdateResponse> UpdateChat(ChatUpdateRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a message to a chat.
    /// </summary>
    /// <param name="request">The message sending parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<SendMessageResponse> SendMessage(SendMessageRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a file to a chat.
    /// </summary>
    /// <param name="request">The file sending parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<SendMessageResponse> SendFile(SendFileRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an image to a chat.
    /// </summary>
    /// <param name="request">The image sending parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<SendMessageResponse> SendImage(SendFileRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an album to a chat.
    /// </summary>
    /// <param name="request">The album sending parameters.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    Task<SendMessageResponse> SendAlbum(SendAlbumRequest request, CancellationToken cancellationToken = default);
}