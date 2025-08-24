using Steps.Client.Features.Common;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Client.Features.EntityFeature.SoloResultFeature.Services;

public class SoloResultsDialogManager : DialogManagerBase<SoloResult, SoloResultViewModel, CreateSoloResultViewModel, UpdateSoloResultViewModel>
{
    public SoloResultsDialogManager(SoloResultsManager manager) : base(manager)
    {
    }
}
