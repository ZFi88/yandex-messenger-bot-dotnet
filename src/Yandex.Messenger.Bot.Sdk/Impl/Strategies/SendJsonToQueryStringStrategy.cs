namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

using System.Web;
using Extensions;

/// <inheritdoc />
internal class SendJsonToQueryStringStrategy : ISendStrategy
{
    private readonly string _endpoint;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendJsonStrategy"/> class.
    /// </summary>
    /// <param name="endpoint">An endpoint relative url.</param>
    public SendJsonToQueryStringStrategy(string endpoint)
    {
        _endpoint = endpoint;
    }

    /// <inheritdoc/>
    public HttpRequestMessage CreateRequest(object payload)
    {
        var properties = payload.GetType()
            .GetProperties()
            .Where(p => p.GetValue(payload, null) != null)
            .Select(p => $"{p.Name.ToSnakeCase()}={HttpUtility.UrlEncode(p.GetValue(payload, null).ToString())}");
        var queryString = string.Join("&", properties.ToArray());

        return new HttpRequestMessage(HttpMethod.Get, $"{_endpoint}?{queryString}");
    }
}