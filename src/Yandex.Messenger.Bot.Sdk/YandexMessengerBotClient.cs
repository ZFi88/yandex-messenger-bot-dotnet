namespace Yandex.Messenger.Bot.Sdk;

using System.Net.Http.Headers;
using Abstractions;
using Impl;

/// <inheritdoc />
public class YandexMessengerBotClient : IYandexMessengerBotClient
{
    /// <summary>
    /// Base Yandex Messenger Bot API address.
    /// </summary>
    public const string YandexMessengerBotApiBaseAddress = "https://botapi.messenger.yandex.net/bot/v1/";

    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="YandexMessengerBotClient"/> class.
    /// </summary>
    /// <param name="token">An access token.</param>
    public YandexMessengerBotClient(string token)
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri(YandexMessengerBotApiBaseAddress),
        };
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", token);
        Init();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="YandexMessengerBotClient"/> class.
    /// </summary>
    /// <param name="httpClient">A http client.</param>
    public YandexMessengerBotClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        Init();
    }

    /// <inheritdoc />
    public IChats Chats { get; private set; } = null!;

    /// <inheritdoc />
    public IPolls Polls { get; private set; } = null!;

    /// <inheritdoc />
    public IUpdates Updates { get; private set; } = null!;

    /// <inheritdoc />
    public void Dispose()
    {
        _httpClient.Dispose();
    }

    private void Init()
    {
        Chats = new Chats(_httpClient);
        Polls = new Polls(_httpClient);
        Updates = new Updates(_httpClient);
    }
}