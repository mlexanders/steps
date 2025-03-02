using Steps.Domain.Base;

namespace Steps.Shared.Contracts.Clubs.ViewModels;

public class ClubViewModel : IHaveId
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
}