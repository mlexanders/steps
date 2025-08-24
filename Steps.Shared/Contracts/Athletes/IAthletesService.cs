using Steps.Domain.Entities;
using Steps.Shared.Contracts.Athletes.ViewModels;

namespace Steps.Shared.Contracts.Athletes;

public interface IAthletesService :
    ICrudService<Athlete, AthleteViewModel, CreateAthleteViewModel, UpdateAthleteViewModel>
{
    Task<Result<List<Guid>>> GetRemovedAthletes();
    Task<Result<byte[]>> GenerateAthleteLabel(string athleteName, string teamName);
}
