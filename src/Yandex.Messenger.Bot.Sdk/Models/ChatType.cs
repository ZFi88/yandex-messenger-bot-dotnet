namespace Yandex.Messenger.Bot.Sdk.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a type of a chat.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ChatType
{
    /// <summary>
    /// Private chat with a user.
    /// </summary>
    Private,

    /// <summary>
    /// Group chat.
    /// </summary>
    Group,

    /// <summary>
    /// A channel.
    /// </summary>
    Channel
}