namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Represents a group chat members change event.
/// </summary>
/// <param name="NewChatMembers">The users added to the chat.</param>
/// <param name="RemovedChatMembers">The users removed from the chat.</param>
public record ChatMembersUpdate(User[]? NewChatMembers, User[]? RemovedChatMembers);
