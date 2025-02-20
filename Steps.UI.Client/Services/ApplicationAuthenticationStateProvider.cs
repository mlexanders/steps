using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Steps.Shared.Contracts.Accounts.ViewModels;
using Steps.UI.Client.Services.Api;

namespace Steps.UI.Client.Services;

public class ApplicationAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly AccountService _accountService;

    public ApplicationAuthenticationStateProvider(AccountService accountService)
    {
        _accountService = accountService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();

        try
        {
            var result = await _accountService.GetCurrentUser();

            if (result is { IsSuccess: true, Value: not null })
                identity = CreateClaimsFrom(result.Value);
        }
        catch (HttpRequestException ex)
        {
        }
        var state = new AuthenticationState(new ClaimsPrincipal(identity));
        return state;
    }
    
    private static ClaimsIdentity CreateClaimsFrom(UserViewModel user)
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
}