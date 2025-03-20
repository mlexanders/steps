using Steps.Domain.Entities;
using Steps.Shared.Contracts.AthletesElements.ViewModels;

namespace Steps.Shared.Contracts.AthletesElements;

public interface IAthleteElementsService : ICrudService<AthleteElements, AthleteElementsViewModel,
    CreateAthleteElementsViewModel, UpdateAthleteElementsViewModel>
{
    Task<Result<AthleteElementsViewModel>> GetAthleteElements(string degree, string ageCategory, string? type);
}