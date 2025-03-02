using Steps.Domain.Base;

namespace Steps.Shared.Contracts.Clubs.ViewModels;

public class UpdateClubViewModel : IHaveId
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
}