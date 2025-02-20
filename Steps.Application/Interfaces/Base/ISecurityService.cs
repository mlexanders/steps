using Steps.Domain.Base;
using Steps.Domain.Entities;

namespace Steps.Application.Interfaces.Base;

public interface ISecurityService
{
    Task<IUser> GetCurrentUser();
}