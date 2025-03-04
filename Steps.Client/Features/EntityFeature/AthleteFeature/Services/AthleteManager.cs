using Steps.Domain.Entities;
using Steps.Shared.Contracts.Athletes;
using Steps.Shared.Contracts.Athletes.ViewModels;

namespace Steps.Client.Features.EntityFeature.AthleteFeature.Services;

public class AthleteManager : BaseEntityManager<Athlete, AthleteViewModel, CreateAthleteViewModel, UpdateAthleteViewModel>
{
    public AthleteManager(IAthleteService service) : base(service)
    {
    }
}