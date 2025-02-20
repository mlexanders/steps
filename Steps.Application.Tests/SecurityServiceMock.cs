using System.Threading.Tasks;
using Steps.Application.Interfaces;
using Steps.Application.Interfaces.Base;
using Steps.Domain.Base;
using Steps.Domain.Entities;

namespace Steps.Application.Tests;

public class SecurityServiceMock : ISecurityService
{
    public async Task<IUser> GetCurrentUser()
    {
        return new User();
    }
}