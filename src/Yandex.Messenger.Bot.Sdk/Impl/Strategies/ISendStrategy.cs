namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

internal interface ISendStrategy
{
    HttpRequestMessage CreateRequest(object request);
}