using Microsoft.AspNetCore.SignalR;
using Steps.Shared.Contracts.Messaging.Notifications;

namespace Steps.Services.WebApi.Hubs;

public class MessagingHub : Hub
{
    // public async Task Send<T>(T message) where T : INotification
    // {
    //     var method = typeof(T).FullName!;
    //     await Clients.All.SendAsync(method, message);
    // }
}