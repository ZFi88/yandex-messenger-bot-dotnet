namespace Yandex.Messenger.Bot.Sdk.Json;

using System.Text.Json;
using System.Text.RegularExpressions;

/// <summary>
/// The policy for transforming property names with underscores.
/// </summary>
public class UnderscorePolicy : JsonNamingPolicy
{
    /// <summary>
    /// The regular expression for splitting PascalCase property names.
    /// </summary>
    private readonly Regex _myRegex = new Regex("(?<!^)(?=[A-Z])");

    /// <inheritdoc />
    public override string ConvertName(string name)
    {
        var split = _myRegex.Split(name).Select(x => x.ToLowerInvariant());
        return string.Join("_", split);
    }
}