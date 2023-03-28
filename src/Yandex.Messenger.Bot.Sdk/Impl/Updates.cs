namespace Yandex.Messenger.Bot.Sdk.Impl;

using Abstractions;
using Models.Requests;
using Models.Responses;
using Strategies;

internal class Updates : BaseClient, IUpdates
{
    private readonly Dictionary<string, IObserver> _observers = new Dictionary<string, IObserver>();

    public Updates(HttpClient client)
        : base(client)
    {
    }

    public IDictionary<string, IObserver> Observers => _observers;

    public async Task<Response> GetUpdates(GetUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var response = await Send<GetUpdateResponse>(new SendJsonStrategy("messages/getUpdates"), request, cancellationToken)
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
        return await Send<SetWebhookResponse>(new SendJsonStrategy("self/update"), request, cancellationToken)
            .ConfigureAwait(false);
    }

    public void Subscribe(IObserver observer)
    {
        _observers.Add(observer.Message ?? string.Empty, observer);
    }
}