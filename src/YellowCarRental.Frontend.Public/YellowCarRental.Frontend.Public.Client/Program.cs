using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;
using SmartSolutionsLab.YellowCarRental.Frontend.Public.Client;
using SmartSolutionsLab.YellowCarRental.Frontend.Shared.Components;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddServiceDiscovery();

builder.Services.AddSingleton<RentalPriceCalculator>(new RentalPriceCalculator());
builder.Services.AddApiClient(builder.Configuration);

builder.Services.AddTransient<ICurrentUserProfile, CurrentUserProfile>();

builder.Services.AddMudServices();


var app = builder.Build();
var currentUser = app.Services.GetRequiredService<ICurrentUserProfile>();
await currentUser.InitializeAsync();
    
await app.RunAsync();

