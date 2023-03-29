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
        var payload = new GetUpdateRequestInternal()
        {
            Limit = request.Limit, Offset = _offset
        };
        var response = await Send<GetUpdateResponse>(new SendJsonStrategy("messages/getUpdates"), payload, cancellationToken)
            .ConfigureAwait(false);

        foreach (var update in response.Updates)
        {
            if (_observers.TryGetValue(string.Empty, out var observer))
            {
                await observer.OnNewUpdate(update, cancellationToken);
            }

            if (_observers.TryGetValue(update.Text, out observer))
            {
                await observer.OnNewUpdate(update, cancellationToken);
            }
        }

        if (response.Updates.Any() && _observers.Any())
        {
            _offset = response.Updates.Max(x => x.UpdateId) + 1;
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