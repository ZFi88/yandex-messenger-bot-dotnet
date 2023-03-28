namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

using Models.Requests;

internal class SendFileStrategy : BaseSendFileStrategy<SendFileRequest>
{
    protected override string Endpoint => "messages/sendFile";

    protected override string FilePartName => "document";
}