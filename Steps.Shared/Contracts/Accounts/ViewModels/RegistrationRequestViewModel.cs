using Steps.Domain.Definitions;

namespace Steps.Shared.Contracts.Accounts.ViewModels;

public class RegistrationViewModel
{
    public string Name { get; set; }
    public Role Role { get; set; }
    public string Login { get; set; }

    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
}