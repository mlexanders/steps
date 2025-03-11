using Steps.Client.Features.Common;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.Clubs;
using Steps.Shared.Contracts.Clubs.ViewModels;

namespace Steps.Client.Features.EntityFeature.ClubsFeature.Services;

public class ClubsManager : EntityManagerBase<Club, ClubViewModel, CreateClubViewModel, UpdateClubViewModel>
{
    public ClubsManager(IClubsService service) : base(service)
    {
    }
}