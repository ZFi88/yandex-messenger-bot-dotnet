namespace Yandex.Messenger.Bot.Sdk.Impl.Strategies;

using Models.Requests;

internal class SendImageStrategy : BaseSendFileStrategy<SendFileRequest>
{
    protected override string Endpoint => "messages/sendImage";

    protected override string FilePartName => "image";
}