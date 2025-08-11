using System;
using System.Threading.Tasks;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared.Exceptions;

namespace Steps.Application.Tests;

public class UserManagerMock : IUserManager<User>
{
    public async Task<IUser> CreateAsync(User user, string password)
    {
        // Симулируем проверку существования пользователя
        if (user.Login == "existinguser@example.com")
        {
            throw new StepsBusinessException("Пользователь с таким email уже зарегистрирован");
        }

        // Возвращаем созданного пользователя
        return new User
        {
            Id = Guid.NewGuid(),
            Login = user.Login,
            Role = user.Role,
            PasswordHash = "hashed_password"
        };
    }

    public Task UpdateAsync(User user)
    {
        // Симулируем обновление пользователя
        if (user.Login == "nonexistent@example.com")
        {
            throw new StepsBusinessException("Пользователь не найден");
        }

        return Task.CompletedTask;
    }

    public async Task<IUser> Login(string email, string password)
    {
        // Симулируем аутентификацию
        if (email == "nonexistent@example.com")
        {
            throw new AppUserNotFoundException(email);
        }

        if (email == "invalid@example.com" || password == "wrongpassword")
        {
            throw new AppInvalidCredentialsException();
        }

        // Возвращаем успешно аутентифицированного пользователя
        return new User
        {
            Id = Guid.NewGuid(),
            Login = email,
            Role = Role.User,
            PasswordHash = "hashed_password"
        };
    }

    public async Task<IUser> FindByEmailAsync(string email)
    {
        if (email == "nonexistent@example.com")
        {
            return null!;
        }

        return new User
        {
            Id = Guid.NewGuid(),
            Login = email,
            Role = Role.User,
            PasswordHash = "hashed_password"
        };
    }

    public Task<string> GenerateEmailConfirmationTokenAsync(User user)
    {
        return Task.FromResult("mock_token");
    }

    public Task ConfirmEmailAsync(User user, string token, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail)
    {
        throw new NotImplementedException();
    }

    public Task ChangeEmailAsync(User user, string token, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<string> GeneratePasswordResetTokenAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task ResetPasswordAsync(User user, string token, string newPassword)
    {
        throw new NotImplementedException();
    }
}