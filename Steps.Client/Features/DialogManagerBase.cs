using Radzen;

namespace Steps.Client.Features;

public interface IDialogManager<in TViewModel, in TCreateViewModel>
{
    Task<bool> ShowCardDialog(TCreateViewModel model);
    Task<bool> ShowCreateDialog();
    Task<bool> ShowUpdateDialog(TViewModel model);
    Task<bool> ShowDeleteDialog(TViewModel model);
}