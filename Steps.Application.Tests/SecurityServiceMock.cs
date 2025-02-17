using System.Threading.Tasks;
using Steps.Application.Interfaces;
using Steps.Domain.Entities;

namespace Steps.Application.Tests;

public class SecurityServiceMock : ISecurityService
{
    public Task<User> GetCurrentUser()
    {
        return Task.FromResult(new User());
    }
}