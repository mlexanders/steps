using System.Reflection.Metadata;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.ContestsFeature.Services;
using Steps.Client.Features.EntityFeature.EntriesFeature.Services;
using Steps.Client.Features.EntityFeature.GroupBlocksFeature.Dialogs;
using Steps.Client.Features.EntityFeature.SchedulesFeature.FinalScheduleFeature;
using Steps.Client.Features.EntityFeature.SchedulesFeature.Services;
using Steps.Client.Features.EntityFeature.UsersFeature.Services;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Contests.ViewModels;
using Steps.Shared.Contracts.GroupBlocks;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;

namespace Steps.Client.Features.EntityFeature.ContestsFeature.Components;

public partial class ContestCard : BaseNotificate
{
    [Inject] protected EntriesDialogManager EntriesDialogManager { get; set; } = null!;
    [Inject] protected ContestManager ContestManager { get; set; } = null!;
    [Inject] protected UsersManager UsersManager { get; set; } = null!;
    [Inject] protected IGroupBlocksService GroupBlocksService { get; set; } = null!;
    [Inject] protected DialogService DialogService { get; set; } = null!;

    [Parameter] public ContestViewModel Model { get; set; } = null!;

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
    }

    private async Task OpenJudgeDialog()
    {
        var groupBlocks = await GroupBlocksService.GetByContestId(Model.Id);

        var selectedGroupBlock = await DialogService.OpenAsync<SelectGroupBlockDialog>(
            "Выбор группового блока",
            new Dictionary<string, object> { { "GroupBlocks", groupBlocks.Value } },
            new DialogOptions { Width = "500px", Height = "400px" });

        if (selectedGroupBlock is GroupBlockViewModel groupBlock)
        {
            await DialogService.OpenAsync<FinalScheduleByGroupBlock>(
                "Финальное расписание",
                new Dictionary<string, object> { { "GroupBlock", groupBlock } },
                new DialogOptions { Width = "800px", Height = "600px" });
        }
    }
}