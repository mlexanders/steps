using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Steps.UI.Client;
using Steps.UI.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddRadzenComponents();

// builder.Services.AddOptions();
// builder.Services.AddAuthorizationCore();

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

await builder.Build().RunAsync();