namespace Yandex.Messenger.Bot.Sdk.Extensions;

using System.Text.RegularExpressions;

/// <summary>
/// Extensions for String.
/// </summary>
internal static class StringExtensions
{
    /// <summary>
    /// The regular expression for splitting PascalCase property names.
    /// </summary>
    private static readonly Regex MyRegex = new Regex("(?<!^)(?=[A-Z])");

    /// <summary>
    /// Convert PascalCase string to snake_case.
    /// </summary>
    /// <param name="str">A string.</param>
    public static string ToSnakeCase(this string str)
    {
        var split = MyRegex.Split(str).Select(x => x.ToLowerInvariant());
        return string.Join("_", split);
    }
}