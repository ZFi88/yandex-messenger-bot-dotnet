using Yandex.Messanger.Bot.Sdk.Models.Requests;
using Yandex.Messanger.Bot.Sdk.Models.Responses;

namespace Yandex.Messanger.Bot.Sdk.Abstractions;

public interface IUpdates : IObservable
{
    Task<Response> GetUpdates(GetUpdateRequest request, CancellationToken cancellationToken = default);
    Task<SetWebhookResponse> SetWebhook(SetWebhookRequest request, CancellationToken cancellationToken = default);
}