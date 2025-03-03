using Steps.Domain.Base;
using Steps.Domain.Definitions;

namespace Steps.Domain.Entities;

public class User : Entity, IUser
{
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public Role Role { get; set; }
}