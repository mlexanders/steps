using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.GroupBlocksFeature.Services;
using Steps.Client.Features.EntityFeature.SchedulesFeature.Services;
using Steps.Client.Features.EntityFeature.SchedulesFeature.Services;
using Steps.Shared.Contracts.Contests.ViewModels;
using Steps.Shared.Contracts.GroupBlocks;
using Steps.Shared.Contracts.GroupBlocks.ViewModels;
using Steps.Shared.Contracts.ScheduleFile.ViewModel;
using Steps.Shared.Contracts.Teams.ViewModels;

namespace Steps.Client.Features.EntityFeature.GroupBlocksFeature.Components;

public partial class GroupBlocksManage : BaseNotificate
{
    [Inject] protected GroupBlocksDialogManager GroupBlocksDialogManager { get; set; } = null!;
    [Inject] protected IGroupBlocksService GroupBlocksService { get; set; } = null!;
    [Inject] protected PreSchedulerManager PreSchedulerManager { get; set; } = null!;
    [Inject] protected IJSRuntime JSRuntime { get; set; } = null!;
    
    [Parameter] [Required] public ContestViewModel? Contest { get; set; }

    private List<TeamViewModel>? _teams;
    private List<GroupBlockViewModel>? _blocks;
    private bool _isComplete;
    private bool _isDownloading;

    protected override async Task OnInitializedAsync()
    {
        if (Contest is null) return;
        await Init();
    }

    private async Task Init()
    {
        var result = await GroupBlocksService.GetByContestId(Contest.Id);
        if (result?.IsSuccess != true) ShowResultMessage(result);

        _blocks = result?.Value ?? [];
    }

    private static string GetTittle(GroupBlockViewModel block)
    {
        var start = block.StartTime.ToString("t");
        var end = block.EndTime.ToString("t");
        return $"{start} - {end}";
    }

    private async Task GetTeamsForCreateGroupBlocks()
    {
        var result = await GroupBlocksService.GetTeamsForCreateGroupBlocks(Contest.Id);
        if (result?.IsSuccess != true) ShowResultMessage(result);
        _teams = result?.Value ?? [];
    }

    private async Task CreateBlocks()
    {
        var createGroupBlockViewModel = new CreateGroupBlockViewModel
        {
            ContestId = Contest.Id,
            AthletesPerGroup = 5,
            TeamsIds = _teams?.Select(t => t.Id).ToList() ?? [],
        };
        
        var result = await GroupBlocksService.CreateByTeams(createGroupBlockViewModel);
        ShowResultMessage(result);
        await Init();
        StateHasChanged();
    }

    private async Task OnDeleteBlocks()
    {
        if (Contest != null)
        {
            var result = await GroupBlocksService.DeleteByContestId(Contest.Id);
            ShowResultMessage(result);
        }

        await Init();
        StateHasChanged();
    }
    
    private async Task CreateScheduleFile()
    {
        _isDownloading = true;

        try
        {
            if (_blocks.Where(g => g.IsHaveFinalBlock == true).Any())
            {
                var createFinalScheduleFileViewModel = new CreateFinalScheduleFileViewModel
                {
                    GroupBlockIds = _blocks.Where(g => g.IsHaveFinalBlock == true)
                        .Select(t => t.Id).ToList()
                };
                
                var _result = await FinalSchedulerManager.GenerateScheduleFile(createFinalScheduleFileViewModel);
    
                if (_result.IsSuccess && _result.Value.Data != null)
                {
                    await JSRuntime.InvokeVoidAsync(
                        "downloadFile", 
                        _result.Value.FileName ?? "schedule.xlsx", 
                        _result.Value.Data
                    );
                }
                else
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Ошибка", "Не удалось сгенерировать файл с финальным расписанием");
                }
            }
            
            var createPreScheduleFileViewModel = new CreatePreScheduleFileViewModel
            {
                GroupBlockIds = _blocks.Select(g => g.Id).ToList()
            };

            var result = await PreSchedulerManager.GeneratePreScheduleFile(createPreScheduleFileViewModel);

            if (result.IsSuccess && result.Value?.Data != null)
            {
                await JSRuntime.InvokeVoidAsync(
                    "downloadFile", 
                    result.Value.FileName ?? "schedule.xlsx", 
                    result.Value.Data
                );
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Ошибка", "Не удалось сгенерировать файл");
            }
        }
        finally
        {
            _isDownloading = false;
        }
    }
}