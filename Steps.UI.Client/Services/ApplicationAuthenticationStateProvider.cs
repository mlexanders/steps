using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Steps.Shared.Contracts.Accounts.ViewModels;

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
            var state = await _accountService.GetCurrentUser();

            if (state is { IsSuccess: true, Value: not null })
                identity = CreateClaimsFrom(state.Value);
        }
        catch (HttpRequestException ex)
        {
        }

        var result = new AuthenticationState(new ClaimsPrincipal(identity));
        return result;
    }
    
    protected static ClaimsIdentity CreateClaimsFrom(UserViewModel user)
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
    
    private static async Task<AuthenticationState> GetAnonymousState()
    {
        return new AuthenticationState(new ClaimsPrincipal());
    }
}