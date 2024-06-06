using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Front;
using MudBlazor.Services;
using Front.Services.Abstractions;
using Microsoft.JSInterop;
using Front.Services;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddScoped<IAuth, AuthService>();
builder.Services.AddScoped<ISudoService, SuperAdminService>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(x => new HttpClient()
{
    BaseAddress = new Uri("https://localhost:7102/")
});


await builder.Build().RunAsync();
