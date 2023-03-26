namespace Yandex.Messenger.Bot.Sdk.Exceptions;

/// <summary>
/// A bot exception.
/// </summary>
public class BotException : Exception
{
    /// <inheritdoc />
    public BotException()
    {
    }

    /// <inheritdoc />
    public BotException(string? message)
        : base(message)
    {
    }

    /// <inheritdoc />
    public BotException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}