namespace Yandex.Messanger.Bot.Sdk;

using System.Net.Http.Headers;
using Abstractions;
using Impl;

public class YandexBotClient : IYandexBotClient
{
    private const string YandexMessangerBotApiBaseAddress = "https://botapi.messenger.yandex.net/bot/v1/";
    private static HttpClient _httpClient = null!;

    public YandexBotClient(string token)
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri(YandexMessangerBotApiBaseAddress),
        };
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", token);

        Chats = new Chats(_httpClient);
        Polls = new Polls(_httpClient);
        Updates = new Updates(_httpClient);
    }

    public YandexBotClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public IChats Chats { get; }

    public IPolls Polls { get; }

    public IUpdates Updates { get; }
}