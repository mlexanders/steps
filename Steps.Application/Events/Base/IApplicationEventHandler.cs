namespace Steps.Application.Events.Base;

public interface IApplicationEventHandler<in TApplicationEvent> where TApplicationEvent : IApplicationEvent
{
    Task Handle(TApplicationEvent applicationEvent, CancellationToken cancellationToken);
}