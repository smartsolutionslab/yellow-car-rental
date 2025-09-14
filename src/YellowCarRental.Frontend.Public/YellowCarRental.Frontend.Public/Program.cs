using MudBlazor.Services;
using SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;
using SmartSolutionsLab.YellowCarRental.Frontend.Public.Components;
using SmartSolutionsLab.YellowCarRental.Frontend.Shared.Components.Layout;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddOutputCache();

builder.Services.AddApiClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseOutputCache();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SmartSolutionsLab.YellowCarRental.Frontend.Public.Client._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(MainLayout).Assembly)
    .AddAdditionalAssemblies(typeof(VehicleApiClient).Assembly);

app.MapDefaultEndpoints();

app.Run();