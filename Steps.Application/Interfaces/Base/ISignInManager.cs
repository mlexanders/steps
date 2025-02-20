using Steps.Domain.Base;
using Steps.Domain.Entities;

namespace Steps.Application.Interfaces.Base;

public interface ISignInManager : ISecurityService
{
    Task<bool> IsSignedIn();
    Task SignOut();
    Task<IUser> SignInAsync(string email, string password);
}