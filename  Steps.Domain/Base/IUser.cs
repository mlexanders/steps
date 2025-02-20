using Steps.Domain.Definitions;

namespace Steps.Domain.Base;

public interface IUser
{
    public Guid Id { get; set; }
    string Login { get; set; }
    Role Role { get; set; }
}
