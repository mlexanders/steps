using Steps.Domain.Entities;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Shared.Contracts.TestResults;

public interface ISoloResultsService
    : ICrudService<SoloResult, SoloResultViewModel, CreateSoloResultViewModel, UpdateSoloResultViewModel>;
