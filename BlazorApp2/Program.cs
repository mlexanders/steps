using System.Security.Claims;
using BlazorApp2;
using BlazorApp2.Services;
using BlazorApp2.Services.Api;
using BlazorApp2.Services.Api.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddRadzenComponents();


builder.Services.AddOptions();
builder.Services.AddAuthorizationCore(options =>
{
    // options.DefaultPolicy = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
});

builder.Services.AddScoped<AuthenticationStateProvider, ApplicationAuthenticationStateProvider>();


builder.Services.AddScoped(typeof(CookieHandler));
builder.Services.AddHttpClient(
        "Default", 
        opt => opt.BaseAddress = new Uri("http://localhost:5000/api/"))
    .AddHttpMessageHandler<CookieHandler>();

builder.Services.AddScoped<HttpClient>(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("Default");
});
builder.Services.AddScoped(typeof(HttpClientService));
builder.Services.AddScoped(typeof(AccountService));
builder.Services.AddScoped(typeof(SecurityService));
await builder.Build().RunAsync();

namespace BlazorApp2
{
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
            // return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            var state = new AuthenticationState(new ClaimsPrincipal(identity));
            return Task.FromResult(state);
        }
    }
}