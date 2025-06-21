using Microsoft.AspNetCore.Components;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.TestResultFeature.Services;
using Steps.Client.Services.Messaging;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Messaging;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Client.Features.EntityFeature.TestResultFeature.Components
{
    public partial class TestResultList : ManageBaseComponent<TestResult, TestResultViewModel,
        CreateTestResultViewModel, UpdateTestResultViewModel>
    {
        [Inject] protected TestResultsManager TestResultsManager { get; set; } = null!;

        [Inject] protected TestResultsDialogManager TestResultsDialogManager { get; set; } = null!;

        [Inject] protected TestResultCreatedMessaging TestResultCreatedMessage { get; set; } = null!;

        [Parameter] public Guid? ContestId { get; set; }

        private List<TestResultViewModel> _testResults = [];
        private bool _isLoading = true;
        private int _totalCount;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Manager = TestResultsManager;
                DialogManager = TestResultsDialogManager;

                await base.OnInitializedAsync();
                TestResultCreatedMessage.OnReceived += LoadData;
                await TestResultCreatedMessage.StartAsync();

                await LoadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private async Task LoadData(TestResultCreatedMessage message)
        {
            await LoadData();
        }

        protected override async Task<Specification<TestResult>?> GetSpecification()
        {
            return null;
        }

        private async Task LoadData()
        {
            try
            {
                _isLoading = true;

                var specification = new Specification<TestResult>()
                    .Where(r => r.ContestId == ContestId);
                // TestResultsManager.UseSpecification(specification);
                var result = await TestResultsManager.Read(specification);

                if (result.IsSuccess && result.Value != null)
                {
                    _testResults = result.Value.Items.ToList();
                    _totalCount = _testResults.Count;
                }
            }
            finally
            {
                StateHasChanged();
                _isLoading = false;
            }
        }

        private async Task OnRowSelect(TestResultViewModel item)
        {
            if (!ContestId.HasValue) return;

            item.ContestId = ContestId.Value;
            await DialogManager.ShowCardDialog(item);
        }

        public async ValueTask DisposeAsync()
        {
            await TestResultCreatedMessage.StopAsync();
            await TestResultCreatedMessage.DisposeAsync();
        }
    }
}