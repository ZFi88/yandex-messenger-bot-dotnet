namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Opens an URL in a browser.
/// </summary>
/// <param name="Uri">The URL which should be opened.</param>
public record OpenUriDirective(string Uri) : Directive;
