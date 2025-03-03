using Microsoft.AspNetCore.Components;
using Radzen;
using Steps.Domain.Base;

namespace Steps.Client.Features;

public class ManageBaseComponent<TEntity, TViewModel, TCreateViewModel, TUpdateViewModel> : ComponentBase, IDisposable
    where TViewModel : IHaveId
    where TCreateViewModel : class
    where TUpdateViewModel : IHaveId
    where TEntity : class, IHaveId

{
    [Parameter]
    public BaseEntityManager<TEntity, TViewModel, TCreateViewModel, TUpdateViewModel> Manager { get; set; } = null!;

    [Parameter] public IDialogManager<TViewModel> DialogManager { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await Manager.Initialize();
        Manager.ChangedList += StateHasChanged;
    }

    protected virtual async Task OnCreate()
    {
        var result = await DialogManager.ShowCreateDialog();
        if (result) await Manager.LoadPage();
    }

    protected virtual async Task OnUpdate(TViewModel model)
    {
        var result = await DialogManager.ShowUpdateDialog(model);
        if (result) await Manager.LoadPage();
    }

    protected virtual async Task OnDelete(TViewModel model)
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