using System.Net.Http.Headers;
using Yandex.Messanger.Bot.Sdk.Abstractions;
using Yandex.Messanger.Bot.Sdk.Impl;

namespace Yandex.Messanger.Bot.Sdk;

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
    }

    public YandexBotClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public IChats Chats { get; } = new Chats(_httpClient);
    public IPolls Polls { get; } = new Polls(_httpClient);
    public IUpdates Updates { get; } = new Updates(_httpClient);
}