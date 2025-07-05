using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Shared.Contracts.Accounts;

public interface IAccountService
{
    Task<Result> Registration(RegistrationViewModel model);
    Task<Result<UserViewModel>> Login(LoginViewModel model);
    Task<Result<UserViewModel>> GetCurrentUser();
    Task<Result> Logout();
    Task<Result> ChangePassword(ChangePasswordViewModel model);
    Task<Result> ConfirmEmail(string token);
    Task<Result> ResendEmailConfirmation();
}