using Radzen;
using Steps.Client.Features.Common;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Client.Features.EntityFeature.SoloResultFeature.Services;

public class SoloResultsDialogManager : IDialogManager<SoloResultViewModel>
{
    private readonly DialogService _dialogService;
    public SoloResultsDialogManager(DialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public Task<bool> ShowCardDialog(SoloResultViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ShowCreateDialog()
    {
        throw new NotImplementedException();
    }

    public Task<bool> ShowUpdateDialog(SoloResultViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ShowDeleteDialog(SoloResultViewModel model)
    {
        throw new NotImplementedException();
    }
}
