using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Steps.Client.Features.Common;
using Steps.Client.Features.EntityFeature.AthleteFeature.Services;
using Steps.Client.Features.EntityFeature.TestResultFeature.Services;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Athletes.ViewModels;
using Steps.Shared.Contracts.TestResults.ViewModels;

namespace Steps.Client.Features.EntityFeature.TestResultFeature.Components
{
    public partial class TestResultCard : ManageBaseComponent<TestResult, TestResultViewModel, CreateTestResultViewModel, UpdateTestResultViewModel>
    {
        [Inject] protected TestResultsManager TestResultsManager { get; set; } = null!;
        [Inject] protected TestResultsDialogManager TestResultsDialogManager { get; set; } = null!;

        [Inject] protected AthleteManager AthleteManager { get; set; } = null!;

        private AthleteViewModel Athlete {  get; set; }

        protected override async Task OnInitializedAsync()
        {
            Manager = TestResultsManager;
            DialogManager = TestResultsDialogManager;

            var result = await AthleteManager.Read(Model.AthleteId);

            Athlete = result.Value;

            base.OnInitialized();
        }
    }
}
