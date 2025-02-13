using Steps.Domain.Definitions;

namespace Steps.Shared.Contracts.Accounts.ViewModels;

public class UserViewModel
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public Role Role { get; set; }
}