using Steps.Shared.Contracts.Athletes.ViewModels;

namespace Steps.Shared.Contracts.Athletes;
public interface IAthleteService
{
    Task<Result<Guid>> Create(CreateAthleteViewModel createAthleteViewModel);
}
