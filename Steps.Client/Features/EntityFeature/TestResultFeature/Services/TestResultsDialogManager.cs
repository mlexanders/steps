using Radzen;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.Ratings.Components;
using Steps.Client.Features.EntityFeature.TestResultFeature.Components;
using Steps.Client.Features.EntityFeature.TestResultFeature.Dialogs;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;
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

    public async Task<bool> ShowCreateDialog(Guid contestId, Guid athleteId)
    {
        var options = new Dictionary<string, object> { { "ContestId", contestId }, { "AthleteId", athleteId } };
        var result = await _dialogService.OpenAsync<CreateTestResultDialog>("Проставление результата", options);
        return result ?? false;
    }

    public Task<bool> ShowUpdateDialog(TestResultViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ShowDeleteDialog(TestResultViewModel model)
    {
        throw new NotImplementedException();
    }
    
    public async Task<bool> ShowManageDialog(Guid contestId)
    {
        var result = await _dialogService.OpenAsync<TestResultList>("Проставленные результаты",
            new Dictionary<string, object> { { "ContestId", contestId } },
            new DialogOptions { Width = "600px", CloseDialogOnOverlayClick = true });
        
        return result ?? false;
    }

    public async Task<bool> ShowResults(GroupBlockViewModel selectedGroupBlock)
    {
        var result = await _dialogService.OpenAsync<RatingsByGroupBlock>(
                    "Проставленные результаты",
                    new Dictionary<string, object> { { "GroupBlock", selectedGroupBlock } },
                    new DialogOptions { Width = "800px", Height = "600px" });

        return result ?? false;
    }
}