using Steps.Domain.Entities;
using Steps.Shared.Contracts.Clubs;
using Steps.Shared.Contracts.Clubs.ViewModels;

namespace Steps.Client.Features.Organizer.Services.Clubs;

public class ClubsManager : BaseEntityManager<Club, ClubViewModel, CreateClubViewModel, UpdateClubViewModel>
{
    public ClubsManager(IClubsService service) : base(service)
    {
    }
}