using Steps.Application.Events.Base;

namespace Steps.Services.WebApi.Services;

public class ApplicationEventPublisher : IApplicationEventPublisher
{
    private readonly ApplicationEventQueue _applicationEventQueue;

    public ApplicationEventPublisher(ApplicationEventQueue applicationEventQueue)
    {
        _applicationEventQueue = applicationEventQueue;
    }
    
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IApplicationEvent
    {
        await _applicationEventQueue.Writer<TEvent>().WriteAsync(@event, cancellationToken);
    }
}