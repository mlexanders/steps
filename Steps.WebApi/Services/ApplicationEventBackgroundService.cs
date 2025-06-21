using System.Reflection;
using Steps.Application.Events.Base;

namespace Steps.Services.WebApi.Services;

public class ApplicationEventBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ApplicationEventQueue _eventQueue;
    private readonly ILogger<ApplicationEventBackgroundService> _logger;

    public ApplicationEventBackgroundService(
        IServiceProvider serviceProvider,
        ApplicationEventQueue eventQueue,
        ILogger<ApplicationEventBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _eventQueue = eventQueue;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        
        var handlerTypes = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IApplicationEventHandler<>))
                .Select(i => new
                {
                    EventType = i.GenericTypeArguments[0],
                    HandlerType = t
                }))
            .ToList();

        foreach (var pair in handlerTypes)
        {
            var method = typeof(ApplicationEventBackgroundService)
                .GetMethod(nameof(ListenAsync), BindingFlags.Instance | BindingFlags.NonPublic)!
                .MakeGenericMethod(pair.EventType);

            _ = (Task)method.Invoke(this, [stoppingToken])!;
        }
    }

    private async Task ListenAsync<TEvent>(CancellationToken token)
        where TEvent : class, IApplicationEvent
    {
        var reader = _eventQueue.Reader<TEvent>();

        await foreach (var @event in reader.ReadAllAsync(token))
        {
            using var scope = _serviceProvider.CreateScope();

            var handler = scope.ServiceProvider.GetRequiredService<IApplicationEventHandler<TEvent>>();

            try
            {
                await handler.Handle(@event, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling event {EventType}", typeof(TEvent).Name);
            }
        }
    }
}