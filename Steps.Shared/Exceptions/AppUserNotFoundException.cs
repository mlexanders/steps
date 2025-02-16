namespace Steps.Shared.Exceptions;

public class UserNotFoundException : StepsBusinessException
{
    public UserNotFoundException(string email) : base($"Пользователь c email: {email} не найден")
    {
    }
}

public class AppAccessDeniedException : StepsBusinessException
{
    public AppAccessDeniedException() : base("Доступ запрещен")
    {
    }
}

public class InvalidCredentialsException : StepsBusinessException
{
    public InvalidCredentialsException() : base($"Неверный логин или пароль")
    {
    }
}

public class StepsBusinessException : Exception
{
    public StepsBusinessException(string message) : base(message)
    {
    }
}