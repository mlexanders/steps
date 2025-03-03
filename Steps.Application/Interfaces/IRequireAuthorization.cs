using Steps.Domain.Base;
using Steps.Domain.Entities;

namespace Steps.Application.Interfaces;

public interface IRequireAuthorization
{
    Task<bool> CanAccess(IUser user);
}