using System.Reflection.Metadata;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Radzen;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.ContestsFeature.Services;
using Steps.Client.Features.EntityFeature.EntriesFeature.Services;
using Steps.Client.Features.EntityFeature.GroupBlocksFeature.Dialogs;
using Steps.Client.Features.EntityFeature.Ratings.Components;
using Steps.Client.Features.EntityFeature.GroupBlocksFeature.Services;
using Steps.Client.Features.EntityFeature.SchedulesFeature.FinalScheduleFeature;
using Steps.Client.Features.EntityFeature.SchedulesFeature.Services;
using Steps.Client.Features.EntityFeature.TestResultFeature.Components;
using Steps.Client.Features.EntityFeature.TestResultFeature.Services;
using Steps.Client.Features.EntityFeature.UsersFeature.Services;
using Steps.Client.Services.Api;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Contests.ViewModels;
using Steps.Shared.Contracts.GroupBlocks;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;
using Steps.Client.Features.EntityFeature.GroupBlocksFeature.Components;
using Steps.Domain.Base;

namespace Steps.Client.Features.EntityFeature.ContestsFeature.Components;

public partial class ContestCard : BaseNotificate
{
    [Inject] protected EntriesDialogManager EntriesDialogManager { get; set; } = null!;
    [Inject] protected ContestManager ContestManager { get; set; } = null!;
    [Inject] protected UsersManager UsersManager { get; set; } = null!;
    [Inject] protected IGroupBlocksService GroupBlocksService { get; set; } = null!;
    [Inject] protected FinalSchedulerDialogManager FinalSchedulerDialogManager { get; set; } = null!;
    [Inject] protected GroupBlocksDialogManager GroupBlocksDialogManager { get; set; } = null!;
    [Inject] protected TestResultsDialogManager TestResultsDialogManager { get; set; } = null!;
    [Inject] protected DialogService DialogService { get; set; } = null!;
    [Inject] protected IJSRuntime JSRuntime { get; set; } = null!;

    [Parameter] public ContestViewModel Model { get; set; } = null!;
    [CascadingParameter] public IUser? User { get; set; }
    
    private Role? UserRole => User?.Role;

    private List<UserViewModel> Judges { get; set; } = new();
    private List<UserViewModel> Counters { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {

        var specification = new Specification<Contest>().Include(c =>
            c.Include(j => j.Judges).Include(c => c.Counters));

        ContestManager.UseSpecification(specification);

        var contest = await ContestManager.Read(Model.Id);

        if (contest.Value is { JudjesIds.Count: > 0 })
        {
            var judgeIds = contest.Value.JudjesIds.ToArray();
            var judgeSpecification = new Specification<User>()
                .Where(j => j.Role == Role.Judge && judgeIds.Contains(j.Id));

            var judges = await UsersManager.Read(judgeSpecification);
            Judges = judges?.Value?.Items?.ToList() ?? new List<UserViewModel>();
        }

        if (contest.Value is { CountersIds.Count: > 0 })
        {
            var counterIds = contest.Value.CountersIds.ToArray();
            var counterSpecification = new Specification<User>()
                .Where(j => j.Role == Role.Counter && counterIds.Contains(j.Id));

            var counters = await UsersManager.Read(counterSpecification);
            Counters = counters?.Value?.Items?.ToList() ?? new List<UserViewModel>();
        }

        await base.OnInitializedAsync();
    }

    protected async Task OpenCreateEntryDialog()
    {
        var result = await EntriesDialogManager.ShowCreateDialog(Model.Id);
        if (result) await ContestManager.LoadPage();
    }

    private async Task CloseCollectingEntries()
    {
        var result = await ContestManager.CloseContest(Model.Id);
        ShowResultMessage(result);
        StateHasChanged();
    }

    
    //todo: я же вроде менял, проверить коммиты
    private async Task OpenJudgeDialog()
    {
        var groupBlocks = await GroupBlocksService.GetByContestId(Model.Id);

        var selectedGroupBlock = await GroupBlocksDialogManager.ShowSelectGroupBlockDialog(
            groupBlocks.Value);

        if (selectedGroupBlock != null && Model.Type == ContestType.Test)
        {
            await DialogService.OpenAsync<TestResultCreating>(
                "Финальное расписание",
                new Dictionary<string, object> { { "GroupBlock", selectedGroupBlock } },
                new DialogOptions { Width = "800px", Height = "600px" });
        }

        if (Model.Type == ContestType.Solo)
        {
        }
    }

    private async Task OpenCounterDialog()
    {
        await DialogService.OpenAsync<TestResultList>(
            "Проставленные результаты",
            new Dictionary<string, object> { { "ContestId", Model.Id } },
            new DialogOptions { Width = "800px", Height = "600px" });
    }
    
    private async Task OpenDiplomaDialog()
    {
        var groupBlocks = await GroupBlocksService.GetByContestId(Model.Id);

        if (groupBlocks.Value != null)
        {
            var selectedGroupBlock = await GroupBlocksDialogManager.ShowSelectGroupBlockDialog(
                groupBlocks.Value);

            if (selectedGroupBlock != null)
            {
                await DialogService.OpenAsync<RatingsByGroupBlock>(
                    "Проставленные результаты",
                    new Dictionary<string, object> { { "GroupBlock", selectedGroupBlock} },
                    new DialogOptions { Width = "800px", Height = "600px" });
            }
        }
    }

    private async Task OpenGroupBlocksDialog()
    {
        await GroupBlocksDialogManager.ShowGroupBlocksDialogManage(Model);
    }
    
    private async Task DownloadPreScheduleFileAsync()
    {
        if (Model.PreScheduleFile?.Data != null && Model.PreScheduleFile.Data.Length > 0)
        {
            var base64String = Convert.ToBase64String(Model.PreScheduleFile.Data);
            await JSRuntime.InvokeVoidAsync("downloadFile", Model.PreScheduleFile.FileName ?? "Schedule.xlsx", base64String);
        }
    }
    
    private async Task DownloadFinalScheduleFileAsync()
    {
        if (Model.FinalScheduleFile?.Data != null && Model.FinalScheduleFile.Data.Length > 0)
        {
            var base64String = Convert.ToBase64String(Model.FinalScheduleFile.Data);
            await JSRuntime.InvokeVoidAsync("downloadFile", Model.FinalScheduleFile.FileName ?? "Schedule.xlsx", base64String);
        }
    }
}