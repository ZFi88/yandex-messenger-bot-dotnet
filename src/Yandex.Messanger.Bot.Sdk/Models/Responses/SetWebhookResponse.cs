﻿namespace Yandex.Messanger.Bot.Sdk.Models.Responses;

public record SetWebhookResponse(bool Ok, string Description) : Response(Ok, Description);