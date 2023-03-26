namespace Yandex.Messanger.Bot.Sdk.Models;

using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ChatType
{
    Private,
    Group,
    Channel
}