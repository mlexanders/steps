using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Steps.Domain.Base;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.Shared.Utils;

namespace Steps.UI.Client.Services;

public class ApplicationAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
{
    private readonly SecurityService _securityService;

    public ApplicationAuthenticationStateProvider(SecurityService securityService)
    {
        _securityService = securityService;
        _securityService.OnUserChanged += OnChangeState;
    }

    private void OnChangeState()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

        try
        {
            var user = await _securityService.GetCurrentUser();

            if (user is not null)
                identity = CreateClaimsFrom(user);
        }
        catch (HttpRequestException ex)
        {
        }
        
        var state = new AuthenticationState(new ClaimsPrincipal(identity));
        return state;
    }
    
    private static ClaimsIdentity CreateClaimsFrom(IUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Login),
            new(ClaimTypes.Sid, user.Id.ToString()),
            new(ClaimTypes.Email, user.Login),
            new(ClaimTypes.Role, user.Role.ToString()),
        };

        return new ClaimsIdentity(claims, "Some-claims");
    }

    public void Dispose()
    {
        _securityService.OnUserChanged -= OnChangeState;
    }
}