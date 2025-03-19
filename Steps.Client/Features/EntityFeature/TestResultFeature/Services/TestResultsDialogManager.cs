using Radzen;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.TestResultFeature.Components;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Client.Features.EntityFeature.TestResultFeature.Services;

public class TestResultsDialogManager : IDialogManager<TestResultViewModel>
{
    private readonly DialogService _dialogService;

    public TestResultsDialogManager(DialogService dialogService)
    {
        _dialogService = dialogService;
    }
    
    public async Task<bool> ShowCardDialog(TestResultViewModel model)
    {
        var result = await _dialogService.OpenAsync<TestResultCard>("Оценка",
            new Dictionary<string, object> { { "Model", model } },
            new DialogOptions { Width = "600px", CloseDialogOnOverlayClick = true });
        
        return result ?? false;
    }

    public Task<bool> ShowCreateDialog()
    {
        throw new NotImplementedException();
    }

    public Task<bool> ShowUpdateDialog(TestResultViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ShowDeleteDialog(TestResultViewModel model)
    {
        throw new NotImplementedException();
    }
}