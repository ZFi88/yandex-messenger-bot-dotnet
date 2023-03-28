namespace Yandex.Messenger.Bot.Sdk.Json;

using System.Text.Json;
using System.Text.RegularExpressions;

public partial class SerializePolicy : JsonNamingPolicy
{
    private readonly Regex _myRegex = new Regex("(?<!^)(?=[A-Z])");

    public override string ConvertName(string name)
    {
        var split = _myRegex.Split(name).Select(x => x.ToLowerInvariant());
        return string.Join("_", split);
    }
}