using Microsoft.AspNetCore.Components;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared;
using Steps.Shared.Contracts.Schedules.FinalSchedulesFeature.ViewModels;

namespace Steps.Client.Features.EntityFeature.SchedulesFeature.FinalScheduleFeature;

public partial class FinalScheduleByGroupBlockForJudge : FinalScheduleByGroupBlock
{
    [Parameter] public FinalScheduledCellViewModel? FinalScheduledCell { get; set; }
    [Parameter] public EventCallback<FinalScheduledCellViewModel> FinalScheduledCellChanged { get; set; }

    protected override async Task OnInitializedAsync()
    {
        FinalSchedulerManager.UseSpecification(new Specification<FinalScheduledCell>()
            .Where(c => !c.HasScore));
        await base.OnInitializedAsync();
    }

    protected override Task OnRowSelect(FinalScheduledCellViewModel? athlete)
    {
        FinalScheduledCell = athlete;
        return FinalScheduledCellChanged.HasDelegate
            ? FinalScheduledCellChanged.InvokeAsync(FinalScheduledCell)
            : Task.CompletedTask;
    }

    public Task Update()
    {
        return FinalSchedulerManager.LoadPage();
    }
}