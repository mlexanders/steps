using System;
using System.Threading.Tasks;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Base;
using Steps.Domain.Entities;

namespace Steps.Application.Tests;

public class UserManagerMock : IUserManager<User>
{
    public async Task<IUser> CreateAsync(User user, string password)
    {
        return new User();
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<IUser> Login(string email, string password)
    {
        return new User();
    }

    public async Task<IUser> FindByEmailAsync(string email)
    {
        return new User();
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