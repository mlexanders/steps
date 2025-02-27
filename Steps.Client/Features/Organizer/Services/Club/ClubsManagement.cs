using Steps.Shared.Contracts.Clubs;
using Steps.Shared.Contracts.Clubs.ViewModels;

namespace Steps.Client.Features.Organizer.Services.Club;

public class ClubsManager : BaseEntityManager<ClubViewModel, CreateClubViewModel, UpdateClubViewModel>
{
    public ClubsManager(IClubsService service) : base(service)
    {
    }
}