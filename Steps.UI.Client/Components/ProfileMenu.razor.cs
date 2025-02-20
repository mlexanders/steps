using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Steps.Domain.Base;

namespace Steps.UI.Client.Components;

public partial class ProfileMenu
{
    [CascadingParameter] private IUser CurrentUser { get; set; } = null!;

    private async Task Logout(RadzenProfileMenuItem args)
    {
        if (args.Value == "logout") NavigationManager.NavigateTo("/logout");
    }
}