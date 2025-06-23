using Microsoft.AspNetCore.SignalR.Client;
using Steps.Shared.Contracts.Messaging.Notifications;

namespace Steps.Client.Services.Messaging.Base;

public abstract class SignalRClientBase<T> : IAsyncDisposable where T : MessageContract<T>
{
    private readonly string _methodName = typeof(T).FullName!;
    private readonly HubConnection _hubConnection;

    protected SignalRClientBase(string url)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(url, options =>
            {
                options.SkipNegotiation = true;
                options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
            })
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.On<T>(_methodName, HandleMessageReceive);
    }

    protected abstract Task HandleMessageReceive(T message);

    public Task StartAsync() => _hubConnection.StartAsync();
    public Task StopAsync() => _hubConnection.StopAsync();

    public async ValueTask DisposeAsync() => await _hubConnection.DisposeAsync();
}