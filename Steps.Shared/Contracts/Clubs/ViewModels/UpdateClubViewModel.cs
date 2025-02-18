namespace Steps.Shared.Contracts.Clubs.ViewModels;

public class UpdateClubViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
}