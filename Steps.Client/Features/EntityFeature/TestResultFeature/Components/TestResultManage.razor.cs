using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.AthleteFeature.Services;
using Steps.Client.Features.EntityFeature.EntriesFeature.Services;
using Steps.Client.Features.EntityFeature.TestResultFeature.Services;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.TestResults;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Client.Features.EntityFeature.TestResultFeature.Components
{
    public partial class TestResultManage : ManageBaseComponent<TestResult, TestResultViewModel, CreateTestResultViewModel, UpdateTestResultViewModel>
    {
        [Inject] protected TestResultsManager TestResultsManager { get; set; } = null!;
        [Inject] protected TestResultsDialogManager TestResultsDialogManager { get; set; } = null!;
        
        [Inject] protected NavigationManager Navigation { get; set; } = null!;

        //[Inject] protected IHubContext<TestResultHub> HubContext { get; set; } = null!;

        [Parameter] public Guid ContestId { get; set; }

        private List<TestResultViewModel> _testResults = new();
        private bool _isLoading = true;
        private int _totalCount = 0;

        //private HubConnection? _hubConnection;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Manager = TestResultsManager;
                DialogManager = TestResultsDialogManager;

                var specification = new Specification<TestResult>()
                    .Where(r => r.ContestId == ContestId);
            
                var result = await TestResultsManager.Read(specification);

                if (result.IsSuccess && result.Value != null)
                {
                    _testResults = result.Value.Items
                        .Select(r => new TestResultViewModel
                        {
                            AthleteId = r.AthleteId,
                            Scores = r.Scores
                        })
                        .ToList();

                    _totalCount = _testResults.Count;
                }

                _isLoading = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка инициализации: {ex.Message}");
            }
        }

        private async Task OnRowSelect(TestResultViewModel item)
        {
            item.ContestId = ContestId;

            await DialogManager.ShowCardDialog(item);
        }

        //private async Task SetupSignalR()
        //{
        //    _hubConnection = new HubConnectionBuilder()
        //        .WithUrl(Navigation.ToAbsoluteUri("/testResultsHub")) //todo: нужно ip 
        //        .Build();

        //    _hubConnection.On<TestResultViewModel>("ReceiveTestResult", result =>
        //    {
        //        if (result.ContestId == ContestId)
        //        {
        //            Manager.Data.Add(result);
        //            StateHasChanged();
        //        }
        //    });

        //    await _hubConnection.StartAsync();
        //}

        //public async ValueTask DisposeAsync()
        //{
        //    if (_hubConnection != null)
        //    {
        //        await _hubConnection.DisposeAsync();
        //    }
        //}
    }
}
