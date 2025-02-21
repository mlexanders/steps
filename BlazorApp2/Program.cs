using System.Security.Claims;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp2;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// builder.Services.AddMsalAuthentication(options =>
// {
//     builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
// });



builder.Services.AddOptions();
builder.Services.AddAuthorizationCore(options =>
{
    // options.DefaultPolicy = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
});

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
await builder.Build().RunAsync();

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, "user.Login"),
            new(ClaimTypes.Sid, "user.Id.ToString()"),
            new(ClaimTypes.Email, "user.Login"),
            new(ClaimTypes.Role, "user.Role.ToString()"),
        };
        identity.AddClaims(claims);
        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
        var state = new AuthenticationState(new ClaimsPrincipal(identity));
        return Task.FromResult(state);
    }
}