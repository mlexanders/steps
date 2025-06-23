using Steps.Client.Features.Common;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Athletes;
using Steps.Shared.Contracts.Athletes.ViewModels;

namespace Steps.Client.Features.EntityFeature.AthleteFeature.Services;

public class
    AthleteManager : EntityManagerBase<Athlete, AthleteViewModel, CreateAthleteViewModel, UpdateAthleteViewModel>
{
    public AthleteManager(IAthletesService service) : base(service)
    {
    }
}