namespace Yandex.Messenger.Bot.AspNetCore.Services;

using Sdk.Models;

/// <summary>
/// An update handler.
/// </summary>
internal interface IUpdateHandler
{
    /// <summary>
    /// Handles the update.
    /// </summary>
    /// <param name="update">The Update.</param>
    void Handle(Update update);
}