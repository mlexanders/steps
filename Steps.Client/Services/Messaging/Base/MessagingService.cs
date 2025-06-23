using Steps.Shared.Contracts.Messaging.Notifications;

namespace Steps.Client.Services.Messaging.Base;

public class MessagingService<T> : SignalRClientBase<T> where T : MessageContract<T>
{
    public event Func<T, Task>? OnReceived;

    protected MessagingService(string url) : base(url) { }

    protected override async Task HandleMessageReceive(T message)
    {
        try
        {
            var handler = OnReceived;

            if (handler == null)
            {
                return;
            }

            var invocationList = handler.GetInvocationList();
            var handlerTasks = new Task[invocationList.Length];

            for (int i = 0; i < invocationList.Length; i++)
            {
                handlerTasks[i] = ((Func<T, Task>)invocationList[i])(message);
            }

            await Task.WhenAll(handlerTasks);
        }
        catch (Exception e)
        {
            throw;
        }
    }
}