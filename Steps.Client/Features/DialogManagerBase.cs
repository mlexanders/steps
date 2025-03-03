using Radzen;

namespace Steps.Client.Features;

public interface IDialogManager<in TViewModel>
{
    Task<bool> ShowCardDialog(TViewModel model);
    Task<bool> ShowCreateDialog();
    Task<bool> ShowUpdateDialog(TViewModel model);
    Task<bool> ShowDeleteDialog(TViewModel model);
}