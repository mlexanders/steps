using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
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
        await Task.Delay(1200);
        try
        {
            var user = await _securityService.GetCurrentUser();

            if (user is not null)
            {
                var identity = GetClaimsFrom(user);
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
        }
        catch
        {
            // ignored
        }

        return GetAnonymousState();
    }

    private static AuthenticationState GetAnonymousState()
    {
        return new AuthenticationState(new ClaimsPrincipal());
    }

    private static ClaimsIdentity GetClaimsFrom(IUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Login),
            new(ClaimTypes.Sid, user.Id.ToString()),
            new(ClaimTypes.Email, user.Login),
            new(ClaimTypes.Role, user.Role.ToString()),
        };

        return new ClaimsIdentity(claims, "Cookies", ClaimTypes.Name,
            ClaimTypes.Role);
    }

    public void Dispose()
    {
        _securityService.OnUserChanged -= OnChangeState;
    }
}