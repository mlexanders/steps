using Steps.Domain.Entities;

namespace Steps.Application.Interfaces;

public interface IRequireAuthorization
{
    Task<bool> CanAccess(User user);
}