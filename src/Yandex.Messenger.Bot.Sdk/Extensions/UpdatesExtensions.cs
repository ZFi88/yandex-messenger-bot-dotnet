namespace Yandex.Messenger.Bot.Sdk.Extensions;

using Abstractions;
using Impl;
using Models;

public static class UpdatesExtensions
{
    public static void Subscribe(this IUpdates updates, Func<Update, Task> func)
    {
        updates.Subscribe(new Observer(null!, func));
    }

    public static void Subscribe(this IUpdates updates, string msg, Func<Update, Task> func)
    {
        updates.Subscribe(new Observer(msg, func));
    }
}