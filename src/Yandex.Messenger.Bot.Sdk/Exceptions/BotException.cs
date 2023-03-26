namespace Yandex.Messenger.Bot.Sdk.Exceptions;

public class BotException : Exception
{
    public BotException()
    {
    }

    public BotException(string? message)
        : base(message)
    {
    }

    public BotException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}