using Yandex.Messanger.Bot.Sdk.Abstractions;
using Yandex.Messanger.Bot.Sdk.Models.Requests;
using Yandex.Messanger.Bot.Sdk.Models.Responses;

namespace Yandex.Messanger.Bot.Sdk.Impl;

internal class Updates : BaseClient, IUpdates
{
    private readonly Dictionary<string, IObserver> _observers = new Dictionary<string, IObserver>();

    public Updates(HttpClient client) : base(client)
    {
    }

    public async Task<Response> GetUpdates(GetUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var response = await Send<GetUpdateResponse>("messages/sendFile", HttpMethod.Post, request, cancellationToken)
            .ConfigureAwait(false);
        foreach (var update in response.Updates)
        {
            if (_observers.TryGetValue(string.Empty, out var observer))
            {
                await observer.OnNewUpdate(update);
            }

            if (_observers.TryGetValue(update.Text, out observer))
            {
                await observer.OnNewUpdate(update);
            }
        }

        return response;
    }

    public async Task<SetWebhookResponse> SetWebhook(SetWebhookRequest request, CancellationToken cancellationToken = default)
    {
        return await Send<SetWebhookResponse>("self/update", HttpMethod.Post, request, cancellationToken)
            .ConfigureAwait(false);
    }

    public void Subscribe(IObserver observer)
    {
        _observers.TryAdd(observer.Message ?? string.Empty, observer);
    }
}