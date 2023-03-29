namespace Yandex.Messenger.Bot.Sdk.Impl;

using Abstractions;
using Models.Requests;
using Models.Responses;
using Strategies;

internal class Updates : BaseClient, IUpdates
{
    private readonly Dictionary<string, IObserver> _observers = new();
    private long _offset = 0L;

    public Updates(HttpClient client)
        : base(client)
    {
    }

    public IDictionary<string, IObserver> Observers => _observers;

    public async Task<Response> GetUpdates(GetUpdateRequest request, CancellationToken cancellationToken = default)
    {
        request = request with { Offset = _offset };
        var response = await Send<GetUpdateResponse>(new SendJsonStrategy("messages/getUpdates"), request, cancellationToken)
            .ConfigureAwait(false);

        foreach (var update in response.Updates)
        {
            if (_observers.TryGetValue(string.Empty, out var observer))
            {
                await observer.OnNewUpdate(update, cancellationToken);
                _offset = update.UpdateId + 1;
            }

            if (_observers.TryGetValue(update.Text, out observer))
            {
                await observer.OnNewUpdate(update, cancellationToken);
                _offset = update.UpdateId + 1;
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