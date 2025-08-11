using System;
using System.Threading;
using System.Threading.Tasks;
using Steps.Application.Events.Base;

namespace Steps.Application.Tests;

/// <summary>
/// Мок для IApplicationEventPublisher, который выводит события в консоль
/// </summary>
public class ApplicationEventPublisherMock : IApplicationEventPublisher
{
    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : IApplicationEvent
    {
        Console.WriteLine($"[EVENT PUBLISHED] {typeof(TEvent).Name}: {@event} (CancellationToken: {cancellationToken})");
        return Task.CompletedTask;
    }
} 