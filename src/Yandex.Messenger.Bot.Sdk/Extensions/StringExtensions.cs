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
    private static readonly Regex PascalCaseSplitRegex = new Regex(
        @"(?<=[a-z])(?=[A-Z])|(?<=[A-Z])(?=[A-Z][a-z])|(?<=\d)(?=[A-Za-z])");

    /// <summary>
    /// Convert PascalCase string to snake_case.
    /// </summary>
    /// <param name="str">A string.</param>
    public static string ToSnakeCase(this string str)
    {
        var split = PascalCaseSplitRegex.Split(str).Select(x => x.ToLowerInvariant());
        return string.Join("_", split);
    }
}