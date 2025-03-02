using Steps.Domain.Base;
using Steps.Domain.Definitions;

namespace Steps.Shared.Contracts.Users.ViewModels;

public class UpdateUserViewModel : IUser
{
    public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    public Role Role { get; set; }
}