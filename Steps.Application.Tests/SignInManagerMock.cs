using System;
using System.Threading.Tasks;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Base;
using Steps.Domain.Entities;

namespace Steps.Application.Tests;

public class SignInManagerMock : ISignInManager
{
    public async Task<IUser> GetCurrentUser()
    {
        return new User();
    }

    public Task<bool> IsSignedIn()
    {
        return Task.FromResult(true);
    }

    public Task SignOut()
    {
        throw new NotImplementedException();
    }

    public Task<IUser> SignInAsync(string email, string password)
    {
        throw new NotImplementedException();
    }
}