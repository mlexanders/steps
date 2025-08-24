using Steps.Client.Features.Common;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.TestResults;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Client.Features.EntityFeature.SoloResultFeature.Services;

public class SoloResultsManager : EntityManagerBase<SoloResult, SoloResultViewModel, CreateSoloResultViewModel, UpdateSoloResultViewModel>
{
    public SoloResultsManager(ISoloResultsService service) : base(service)
    {
    }
}
