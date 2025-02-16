using Steps.Domain.Entities;

namespace Steps.Application.Interfaces;

public interface ISecurityService
{
    Task<User> GetCurrentUser();
}