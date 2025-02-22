using System;
using System.Net.Http;
using Steps.Client;
using Steps.Client.Services;
using Steps.Client.Services.Api;
using Steps.Client.Services.Api.Base;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRadzenComponents();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

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