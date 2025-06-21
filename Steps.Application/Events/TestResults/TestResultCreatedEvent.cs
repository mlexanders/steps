using Steps.Application.Events.Base;
using Steps.Application.Interfaces;
using Steps.Shared.Contracts.Messaging;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Application.Events.TestResults;

public record TestResultCreatedEvent(TestResultViewModel CreatedTestResult) : IApplicationEvent;

public class TestResultCreatedApplicationEventHandler : IApplicationEventHandler<TestResultCreatedEvent>
{
    private readonly INotificationService _notificationService;

    public TestResultCreatedApplicationEventHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task Handle(TestResultCreatedEvent @event, CancellationToken cancellationToken)
    {
        await _notificationService.Notify(new TestResultCreatedMessage(@event.CreatedTestResult));
    }
}