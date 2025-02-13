using Steps.Domain.Entities;

namespace Steps.Application.Interfaces;

public interface ISignInManager : ISecurityService
{
    Task<bool> IsSignedIn();
    Task SignOut();
    Task<User> SignInAsync(string email, string password);
}