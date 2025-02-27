using Microsoft.AspNetCore.Components;
using Radzen;
using Steps.Client.Features.Organizer.Services;
using Steps.Domain.Base;

namespace Steps.Client.Features.Organizer;

public class ManageBaseComponent<TViewModel, TCreateViewModel, TUpdateViewModel> : ComponentBase, IDisposable
    where TViewModel : IHaveId
    where TCreateViewModel : class
    where TUpdateViewModel : IHaveId
{
    [Parameter] public BaseEntityManager<TViewModel, TCreateViewModel, TUpdateViewModel> Manager { get; set; } = null!;
    [Parameter] public IDialogManager<TViewModel, TCreateViewModel> DialogManager { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await Manager.Initialize();
        Manager.ChangedList += StateHasChanged;
    }

    protected async Task OnCreate()
    {
        var result = await DialogManager.ShowCreateDialog();
        if (result) await Manager.LoadPage();
    }

    protected async Task OnUpdate(TViewModel model)
    {
        var result = await DialogManager.ShowUpdateDialog(model);
        if (result) await Manager.LoadPage();
    }

    protected async Task OnDelete(TViewModel model)
    {
        var result = await DialogManager.ShowDeleteDialog(model);
        if (result) await Manager.LoadPage();
    }

    protected Task OnChangePage(LoadDataArgs arg)
    {
        var skip = arg.Skip;
        var take = arg.Top;

        return Manager.ChangePage(skip, take);
    }

    public void Dispose()
    {
        Manager.ChangedList -= StateHasChanged;
    }
}