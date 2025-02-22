using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Steps.Domain.Base;

namespace Steps.Client.Components;

public partial class ProfileMenu
{
    [CascadingParameter] public IUser CurrentUser { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;

    private void OnAction(RadzenProfileMenuItem args)
    {
        if (args.Value == "logout") NavigationManager.NavigateTo("/logout");
    }
}