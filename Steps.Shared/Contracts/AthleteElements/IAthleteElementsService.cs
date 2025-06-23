using Steps.Shared.Contracts.AthleteElements.ViewModels;

namespace Steps.Shared.Contracts.AthleteElements;

public interface IAthleteElementsService : ICrudService<Domain.Entities.TestAthleteElement, TestAthleteElementsViewModel,
    CreateAthleteElementsViewModel, UpdateAthleteElementsViewModel>
{
    Task<Result<TestAthleteElementsViewModel>> GetAthleteElements(string degree, string ageCategory, string? type);
}