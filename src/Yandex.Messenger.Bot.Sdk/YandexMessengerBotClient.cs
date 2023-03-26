namespace Yandex.Messenger.Bot.Sdk;

using System.Net.Http.Headers;
using Abstractions;
using Impl;

public class YandexMessengerBotClient : IYandexMessangerBotClient
{
    public const string YandexMessengerBotApiBaseAddress = "https://botapi.messenger.yandex.net/bot/v1/";
    private static HttpClient _httpClient = null!;

    public YandexMessengerBotClient(string token)
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri(YandexMessengerBotApiBaseAddress),
        };
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", token);
        Init();
    }

    public YandexMessengerBotClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        Init();
    }

    public IChats Chats { get; private set; }

    public IPolls Polls { get; private set; }

    public IUpdates Updates { get; private set; }

    private void Init()
    {
        Chats = new Chats(_httpClient);
        Polls = new Polls(_httpClient);
        Updates = new Updates(_httpClient);
    }
}