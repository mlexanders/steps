using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Steps.Client.Services;
using Steps.Domain.Base;

namespace Steps.Client.Layout;

public partial class UserState : IDisposable
{
    private IUser? _currentUser;
    [Inject] private SecurityService SecurityService { get; set; } = null!;
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _currentUser = await SecurityService.GetCurrentUser();
        SecurityService.OnUserChanged += OnUserChanged;
    }

    private void OnUserChanged(IUser? user)
    {
        _currentUser = user;
        StateHasChanged();
    }

    public void Dispose()
    {
        SecurityService.OnUserChanged -= OnUserChanged;
    }
}