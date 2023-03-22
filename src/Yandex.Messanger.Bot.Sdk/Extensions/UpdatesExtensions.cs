using Yandex.Messanger.Bot.Sdk.Abstractions;
using Yandex.Messanger.Bot.Sdk.Impl;
using Yandex.Messanger.Bot.Sdk.Models;

namespace Yandex.Messanger.Bot.Sdk.Extensions;

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