namespace Steps.Shared.Contracts.Accounts.ViewModels;

public class ChangePasswordViewModel
{
    public string Password { get; set; } = string.Empty;
    public string PasswordConfirm { get; set; } = string.Empty;
}