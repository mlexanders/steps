using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.ContestsFeature.Services;
using Steps.Client.Features.EntityFeature.EntriesFeature.Dialogs;
using Steps.Client.Features.EntityFeature.EntriesFeature.Services;
using Steps.Client.Features.EntityFeature.UsersFeature.Services;
using Steps.Domain.Definitions;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Contracts.Contests.ViewModels;

namespace Steps.Client.Features.EntityFeature.ContestsFeature.Components;

public partial class ContestCard : ManageBaseComponent<Contest, ContestViewModel, CreateContestViewModel, UpdateContestViewModel>
{
    // [Inject] protected EntriesManagement EntriesManagement { get; set; } = null!;
    [Inject] protected EntriesDialogManager EntriesDialogManager { get; set; } = null!;
    [Inject] protected ContestManager ContestManager { get; set; } = null!;
    [Inject] protected ContestDialogManager ContestDialogManager { get; set; } = null!;
    [Inject] protected UsersManager UsersManager { get; set; } = null!;
    
    [Parameter] public ContestViewModel Model { get; set; } = null!;
    
    private List<UserViewModel> Judges { get; set; } = new();
    private List<UserViewModel> Counters { get; set; } = new();
    
    private CreateEntryDialog _createEntryDialog;

    protected override async Task OnInitializedAsync()
    {
        Manager = ContestManager;
        DialogManager = ContestDialogManager;

        var specification = new Specification<Contest>().Include(c =>
            c.Include(j => j.Judges).Include(c => c.Counters));
        
        Manager.UseSpecification(specification);

        var contest = await Manager.Read(Model.Id);
        
        if (contest.Value.JudjesIds.Any())
        {
            var judgeIds = contest.Value.JudjesIds.ToArray();
            var judgeSpecification = new Specification<User>()
                .Where(j => j.Role == Role.Judge && judgeIds.Contains(j.Id));

            var judges = await UsersManager.Read(judgeSpecification);
            Judges = judges?.Value?.Items?.ToList() ?? new List<UserViewModel>();
        }

        if (contest.Value.CountersIds.Any())
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
        if (result) await Manager.LoadPage();
    }
}