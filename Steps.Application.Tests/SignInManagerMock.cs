using System;
using System.Threading.Tasks;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;

namespace Steps.Application.Tests;

public class SignInManagerMock : ISignInManager
{
    public Task<User> GetCurrentUser()
    {
        return Task.FromResult(new User());
    }

    public Task<bool> IsSignedIn()
    {
        return Task.FromResult(true);
    }

    public Task SignOut()
    {
        throw new NotImplementedException();
    }

    public Task<User> SignInAsync(string email, string password)
    {
        throw new NotImplementedException();
    }
}