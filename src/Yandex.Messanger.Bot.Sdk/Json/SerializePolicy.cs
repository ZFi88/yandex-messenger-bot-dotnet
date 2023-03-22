using System.Text.Json;
using System.Text.RegularExpressions;

namespace Yandex.Messanger.Bot.Sdk.Json;

public partial class SerializePolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        var split = MyRegex().Split(name).Select(x => x.ToLowerInvariant());
        return string.Join('_', split);
    }

    [GeneratedRegex("(?<!^)(?=[A-Z])")]
    private static partial Regex MyRegex();
}