namespace Steps.Shared.Contracts.Messaging.Notifications;

/// <summary>
/// Контракт события
/// </summary>
/// <typeparam name="T">Используется имя типа для названия метода</typeparam>
public class MessageContract<T> : INotification
{
    public string MethodName => typeof(T).FullName ?? typeof(T).Name;
}