using Steps.Domain.Definitions;

namespace Steps.Domain.Base;

public interface IUser : IHaveId
{
    string Login { get; set; }
    Role Role { get; set; }
}
