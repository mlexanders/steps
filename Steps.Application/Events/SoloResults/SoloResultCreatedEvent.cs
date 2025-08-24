using Steps.Application.Events.Base;
using Steps.Application.Interfaces;
using Steps.Shared.Contracts.Messaging;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Application.Events.SoloResults;

public record SoloResultCreatedEvent(SoloResultViewModel CreatedSoloResult) : IApplicationEvent;

public class SoloResultCreatedApplicationEventHandler : IApplicationEventHandler<SoloResultCreatedEvent>
{
    private readonly INotificationService _notificationService;

    public SoloResultCreatedApplicationEventHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task Handle(SoloResultCreatedEvent @event, CancellationToken cancellationToken)
    {
        await _notificationService.Notify(new SoloResultCreatedMessage(@event.CreatedSoloResult));
    }
}
