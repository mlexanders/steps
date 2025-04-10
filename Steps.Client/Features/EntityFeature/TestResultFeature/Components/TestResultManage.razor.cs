using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.TestResultFeature.Services;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Client.Features.EntityFeature.TestResultFeature.Components
{
    public partial class TestResultManage : ManageBaseComponent<TestResult, TestResultViewModel, CreateTestResultViewModel, UpdateTestResultViewModel>
    {
        [Inject] protected TestResultsManager TestResultsManager { get; set; } = null!;
        [Inject] protected TestResultsDialogManager TestResultsDialogManager { get; set; } = null!;
        
        [Inject] protected NavigationManager Navigation { get; set; } = null!;

        [Parameter] public Guid ContestId { get; set; }

        private HubConnection? _hubConnection { get; set; }

        private List<TestResultViewModel> _testResults = new();
        private bool _isLoading = true;
        private int _totalCount = 0;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Manager = TestResultsManager;
                DialogManager = TestResultsDialogManager;
                
                await SetupSignalR();
                
                await LoadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка инициализации: {ex.Message}");
            }
        }
        
        protected override async Task<Specification<TestResult>?> GetSpecification()
        {
            return null;
        }

        private async Task LoadData()
        {
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

        private async Task SetupSignalR()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(Navigation.ToAbsoluteUri("http://localhost:5000/testResultHub"), options =>
                {
                    options.SkipNegotiation = true;
                    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                })
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<TestResultViewModel>("ReceiveTestResult", result =>
            {
                var existing = _testResults.FirstOrDefault(r => r.AthleteId == result.AthleteId);
                if (existing != null)
                {
                    _testResults.Remove(existing);
                }

                _testResults.Add(result);
                StateHasChanged();
            });

            try
            {
                await _hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подключения к SignalR: {ex.Message}");
            }
        }

        private async Task OnRowSelect(TestResultViewModel item)
        {
            item.ContestId = ContestId;

            await DialogManager.ShowCardDialog(item);
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.DisposeAsync();
            }
        }
    }
}
