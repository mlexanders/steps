using Steps.Domain.Base;
using Steps.Domain.Definitions;

namespace Steps.Domain.Entities;

public class User : Entity, IUser
{
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public Role Role { get; set; }
    
    public List<Entry>? Entries { get; set; }
    public List<Contest> JudgingContests { get; set; } = new();
    public List<Contest> CountingContests { get; set; } = new();
}