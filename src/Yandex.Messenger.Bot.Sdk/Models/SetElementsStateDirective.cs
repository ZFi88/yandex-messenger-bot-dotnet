namespace Yandex.Messenger.Bot.Sdk.Models;

/// <summary>
/// Changes the state of interface elements, for example blocks buttons while a request is processed.
/// </summary>
/// <param name="Ids">The identifiers of the elements which should be changed.</param>
/// <param name="State">The new state: <c>disabled</c> or <c>loading</c>.</param>
/// <param name="TimeoutSeconds">The state duration in seconds, from 1 to 60.</param>
public record SetElementsStateDirective(string[] Ids, string State, int? TimeoutSeconds) : Directive;
