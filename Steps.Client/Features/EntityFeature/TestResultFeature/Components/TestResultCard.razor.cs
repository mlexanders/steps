using Microsoft.AspNetCore.Components;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.TestResultFeature.Services;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Client.Features.EntityFeature.TestResultFeature.Components
{
    public partial class TestResultCard : ManageBaseComponent<TestResult, TestResultViewModel, CreateTestResultViewModel, UpdateTestResultViewModel>
    {
        [Inject] protected TestResultsManager TestResultsManager { get; set; } = null!;
        [Inject] protected TestResultsDialogManager TestResultsDialogManager { get; set; } = null!;

        protected override void OnInitialized()
        {
            Manager = TestResultsManager;
            DialogManager = TestResultsDialogManager;

            base.OnInitialized();
        }
    }
}
