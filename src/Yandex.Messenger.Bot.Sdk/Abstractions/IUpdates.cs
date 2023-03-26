namespace Yandex.Messenger.Bot.Sdk.Abstractions;

using Models.Requests;
using Models.Responses;

public interface IUpdates : IObservable
{
    Task<Response> GetUpdates(GetUpdateRequest request, CancellationToken cancellationToken = default);
    Task<SetWebhookResponse> SetWebhook(SetWebhookRequest request, CancellationToken cancellationToken = default);
}