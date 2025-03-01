using Steps.Domain.Base;
using Steps.Domain.Definitions;
using Steps.Shared.Contracts.Entries.ViewModels;

namespace Steps.Shared.Contracts.Accounts.ViewModels;

public class UserViewModel : IUser
{
    public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    public Role Role { get; set; }
}