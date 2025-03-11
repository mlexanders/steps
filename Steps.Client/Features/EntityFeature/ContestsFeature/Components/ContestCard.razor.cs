using Microsoft.AspNetCore.Components;
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

        var judgeSpecification = new Specification<User>().Where(j => j.Role == Role.Judge && Model.JudjesIds.Contains(j.Id));

        var judges = await UsersManager.Read(judgeSpecification);
        
        Judges = judges.Value.Items.ToList();
        
        var counterSpecification = new Specification<User>().Where(j => j.Role == Role.Judge && Model.JudjesIds.Contains(j.Id));

        var counters = await UsersManager.Read(counterSpecification);
        
        Counters = counters.Value.Items.ToList();

        await base.OnInitializedAsync();
    }

    protected override async Task OnCreate()
    {
        var result = await EntriesDialogManager.ShowCreateDialog(Model.Id);
        if (result) await Manager.LoadPage();
    }
}