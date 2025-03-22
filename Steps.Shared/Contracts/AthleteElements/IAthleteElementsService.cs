using Steps.Shared.Contracts.AthleteElements.ViewModels;

namespace Steps.Shared.Contracts.AthleteElements;

public interface IAthleteElementsService : ICrudService<Domain.Entities.AthleteElements, AthleteElementsViewModel,
    CreateAthleteElementsViewModel, UpdateAthleteElementsViewModel>
{
    Task<Result<AthleteElementsViewModel>> GetAthleteElements(string degree, string ageCategory, string? type);
}