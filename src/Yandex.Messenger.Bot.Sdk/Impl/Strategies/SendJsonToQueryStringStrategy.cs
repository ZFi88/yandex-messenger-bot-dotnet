namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

using System.Collections.Concurrent;
using System.Reflection;
using Extensions;

/// <inheritdoc />
internal class SendJsonToQueryStringStrategy : ISendStrategy
{
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> PropertiesCache = new();
    private readonly string _endpoint;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendJsonToQueryStringStrategy"/> class.
    /// </summary>
    /// <param name="endpoint">An endpoint relative url.</param>
    public SendJsonToQueryStringStrategy(string endpoint)
    {
        _endpoint = endpoint;
    }

    /// <inheritdoc/>
    public HttpRequestMessage CreateRequest(object payload)
    {
        var properties = PropertiesCache.GetOrAdd(payload.GetType(), type => type.GetProperties());
        var parts = new List<string>(properties.Length);
        foreach (var property in properties)
        {
            var value = property.GetValue(payload);
            if (value is not null)
            {
                parts.Add($"{property.Name.ToSnakeCase()}={Uri.EscapeDataString(value.ToString())}");
            }
        }

        var queryString = string.Join("&", parts);

        return new HttpRequestMessage(HttpMethod.Get, $"{_endpoint}?{queryString}");
    }
}
