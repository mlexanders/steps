using System;
using System.Threading.Tasks;
using Steps.Application.Interfaces;
using Steps.Shared.Contracts.Messaging.Notifications;

namespace Steps.Application.Tests;

/// <summary>
/// Мок для INotificationService, который выводит уведомления в консоль
/// </summary>
public class NotificationServiceMock : INotificationService
{
    public Task Notify(INotification notification)
    {
        Console.WriteLine($"[NOTIFICATION SENT] MethodName: {notification.MethodName ?? "Default"}");
        throw new NotImplementedException();
    }
} 