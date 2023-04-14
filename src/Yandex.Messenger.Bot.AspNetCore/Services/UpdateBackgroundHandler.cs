namespace Yandex.Messenger.Bot.AspNetCore.Services;

using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sdk.Abstractions;
using Sdk.Models;

/// <inheritdoc cref="IUpdateHandler"/> />
internal sealed class UpdateBackgroundHandler : BackgroundService, IUpdateHandler
{
    private const int MillisecondsDelay = 100;
    private readonly ConcurrentQueue<Update> _updates = new();
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<UpdateBackgroundHandler> _logger;

    /// <inheritdoc />
    public UpdateBackgroundHandler(IServiceScopeFactory serviceScopeFactory, ILogger<UpdateBackgroundHandler> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    /// <inheritdoc/>
    public void Handle(Update update)
    {
        _updates.Enqueue(update);
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_updates.Any())
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var serviceProvider = scope.ServiceProvider;

                var observers = serviceProvider.GetServices<IObserver>();
                var observersLookup = observers.ToLookup(x => x.Message);

                while (_updates.TryDequeue(out var update))
                {
                    var globalObservers = observersLookup[string.Empty];
                    foreach (var observer in globalObservers)
                    {
                        await ProcessUpdate(observer, update, stoppingToken);
                    }

                    var mesObservers = observersLookup[update.Text];
                    foreach (var observer in mesObservers)
                    {
                        await ProcessUpdate(observer, update, stoppingToken);
                    }
                }
            }

            await Task.Delay(MillisecondsDelay, stoppingToken);
        }
    }

    private async Task ProcessUpdate(IObserver observer, Update update, CancellationToken cancellationToken)
    {
        try
        {
            await observer.OnNewUpdate(update, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}