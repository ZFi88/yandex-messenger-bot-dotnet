namespace Yandex.Messenger.Bot.Sdk.Json;

using System.Text.Json;
using Extensions;

/// <summary>
/// The policy for transforming property names with underscores.
/// </summary>
public class UnderscorePolicy : JsonNamingPolicy
{
    /// <inheritdoc />
    public override string ConvertName(string name)
    {
        return name.ToSnakeCase();
    }
}