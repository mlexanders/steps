using Steps.Domain.Entities;

namespace Steps.Application.Interfaces.Base;

public interface ISecurityService
{
    Task<User> GetCurrentUser();
}