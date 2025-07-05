using System;
using System.Threading.Tasks;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;

namespace Steps.Application.Tests;

public class SecurityServiceMock : ISecurityService
{
    public async Task<IUser> GetCurrentUser()
    {
        // Возвращаем мок-пользователя с ролью Organizer для тестирования
        return new User
        {
            Id = Guid.NewGuid(),
            Login = "test@example.com",
            Role = Role.Organizer,
            PasswordHash = "hashed_password"
        };
    }
}