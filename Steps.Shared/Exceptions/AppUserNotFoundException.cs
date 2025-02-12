using System.Security.Authentication;

namespace Steps.Shared.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string email) : base($"Пользователь c email: {email} не найден") { }
}

public class AppAccessDeniedException : AuthenticationException
{
    public AppAccessDeniedException() : base("Доступ запрещен") { }
}

public class InvalidCredentialsException : AuthenticationException
{
    public InvalidCredentialsException() : base($"Неверный логин или пароль") { }
}

