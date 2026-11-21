namespace Yandex.Messenger.Bot.Sdk.Models.Responses;

using Models;

/// <inheritdoc />
/// <param name="ReactionsType">The response type: <c>public</c> or <c>private</c>.</param>
/// <param name="ReactionsList">The reactions with authors. Returned for <c>public</c>.</param>
/// <param name="ReactionsCount">The anonymous reaction counters. Returned for <c>private</c>.</param>
public record GetReactionsResponse(
    bool Ok,
    string Description,
    string ReactionsType,
    MessageReactionEntry[]? ReactionsList,
    ReactionCount[]? ReactionsCount) : Response(Ok, Description);
