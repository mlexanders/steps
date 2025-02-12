using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Shared.Contracts.Accounts;

public interface IAccountService
{
    Task<Result> Registration(RegistrationRequestViewModel model);
    Task<Result> Login(LoginRequestViewModel model);
    Task<Result> Logout();
    Task<Result<string>> ChangePassword(ChangePasswordRequestViewModel model);
    Task<Result> ConfirmAction(string token);
}