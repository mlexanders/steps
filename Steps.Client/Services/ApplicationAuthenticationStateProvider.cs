using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Steps.Domain.Base;

namespace Steps.Client.Services;

public class ApplicationAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
{
    private readonly SecurityService _securityService;

    public ApplicationAuthenticationStateProvider(SecurityService securityService)
    {
        _securityService = securityService;
        _securityService.OnUserChanged += OnChangeState;
    }

    private void OnChangeState(IUser? user)
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name,
            ClaimTypes.Role);

        try
        {
            var user = await _securityService.GetCurrentUser();

            if (user is null)
                return new AuthenticationState(new());
            
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

        return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name,
            ClaimTypes.Role);
    }

    public void Dispose()
    {
        _securityService.OnUserChanged -= OnChangeState;
    }
}