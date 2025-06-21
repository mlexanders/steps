using Microsoft.AspNetCore.SignalR;
using Steps.Application.Interfaces;
using Steps.Services.WebApi.Hubs;
using Steps.Shared.Contracts.Messaging.Notifications;

namespace Steps.Services.WebApi.Services;

public class NotificationService : INotificationService
{
    private readonly IHubContext<MessagingHub> _hubContext;

    public NotificationService(IHubContext<MessagingHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task Notify(INotification notification)
    {
        var method = notification.GetType().FullName!;
        return _hubContext.Clients.All.SendAsync(method, notification);
    }
}