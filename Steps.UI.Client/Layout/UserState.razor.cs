using Microsoft.AspNetCore.Components;
using Steps.Domain.Base;
using Steps.UI.Client.Services;

namespace Steps.UI.Client.Layout;

public partial class UserState
{
    private IUser? _currentUser;
    [Inject] private SecurityService SecurityService { get; set; } = null!;
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _currentUser = await SecurityService.GetCurrentUser();
        SecurityService.OnUserChanged += StateHasChanged;
    }

    public void Dispose()
    {
        SecurityService.OnUserChanged -= StateHasChanged;
    }
}