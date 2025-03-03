using Steps.Shared.Contracts.Accounts.ViewModels;

namespace Steps.Shared.Contracts.Accounts;

public interface IAccountService
{
    Task<Result> Registration(RegistrationViewModel model);
    Task<Result<UserViewModel>> Login(LoginViewModel model);
    Task<Result<UserViewModel>> GetCurrentUser();

    Task<Result> Logout();

    // Task<Result<string>> ChangePassword(ChangePasswordRequestViewModel model);
    Task<Result> ConfirmAction(string token);
}