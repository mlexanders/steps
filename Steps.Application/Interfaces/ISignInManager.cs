namespace Steps.Application.Interfaces;

public interface ISignInManager
{
    Task<bool> IsSignedIn();
    Task SignOut();
    Task SignInAsync(string email, string password);
}