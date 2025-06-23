using Radzen;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.TestResultFeature.Components;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;
using Steps.Shared.Contracts.Schedules.FinalSchedulesFeature.ViewModels;

namespace Steps.Client.Features.EntityFeature.SchedulesFeature.Services;

public class FinalSchedulerDialogManager : IDialogManager<FinalScheduledCellViewModel>
{
    private readonly DialogService _dialogService;

    public FinalSchedulerDialogManager(DialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public Task<bool> ShowCardDialog(FinalScheduledCellViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ShowCreateDialog()
    {
        throw new NotImplementedException();
    }

    public Task<bool> ShowUpdateDialog(FinalScheduledCellViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ShowDeleteDialog(FinalScheduledCellViewModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ShowFinalScheduleByGroupBlockDialogJudge(GroupBlockViewModel model)
    {
        var options = new Dictionary<string, object> { { "GroupBlock", model } };
        
        var result = await _dialogService.OpenAsync<TestResultCreating>(
            "Финальное расписание",
            options,
            new DialogOptions { Width = "800px", Height = "600px" });

        return result;
    }
}