using Microsoft.AspNetCore.Components;

namespace Steps.Client.Layout;

public partial class EmptyLayout
{
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    private void OnBack()
    {
        NavigationManager.NavigateTo("/");
    }
}