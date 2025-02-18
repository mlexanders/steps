namespace Steps.Shared.Exceptions;

public class AppUserNotFoundException : StepsBusinessException
{
    public AppUserNotFoundException(string email) : base($"Пользователь c email: {email} не найден")
    {
    }
}

public class AppAccessDeniedException : StepsBusinessException
{
    public AppAccessDeniedException() : base("Доступ запрещен")
    {
    }
}

public class AppNotFoundException : StepsBusinessException
{
    public AppNotFoundException(string message) : base(message)
    {
    }
}

public class AppInvalidCredentialsException : StepsBusinessException
{
    public AppInvalidCredentialsException() : base($"Неверный логин или пароль")
    {
    }
}

public class StepsBusinessException : Exception
{
    public StepsBusinessException(string message) : base(message)
    {
    }
}

public class AppUnauthorizedAccessException : StepsBusinessException
{
    public AppUnauthorizedAccessException() : base("Неавторизован")
    {
    }
}
