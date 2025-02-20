using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Steps.Domain.Base;

namespace Steps.UI.Client.Components;

public partial class ProfileMenu
{
    [CascadingParameter] public IUser CurrentUser { get; set; }

    private async Task OnAction(RadzenProfileMenuItem args)
    {
        if (args.Value == "logout") NavigationManager.NavigateTo("/logout");
    }
}