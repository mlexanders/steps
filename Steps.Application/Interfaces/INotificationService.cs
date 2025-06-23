using Steps.Shared.Contracts.Messaging.Notifications;

namespace Steps.Application.Interfaces;

public interface INotificationService
{
    Task Notify(INotification notification);
}