using System;
using System.Threading.Tasks;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Entities;

namespace Steps.Application.Tests;

public class UserManagerMock : IUserManager<User>
{
    public Task<Guid> CreateAsync(User user, string password)
    {
        return Task.FromResult(new Guid());
    }

    public Task<User> Login(string email, string password)
    {
        return Task.FromResult(new User());
    }

    public Task<User> FindByEmailAsync(string email)
    {
        return Task.FromResult(new User());
    }

    public Task<string> GenerateEmailConfirmationTokenAsync(User user)
    {
        return Task.FromResult("");
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