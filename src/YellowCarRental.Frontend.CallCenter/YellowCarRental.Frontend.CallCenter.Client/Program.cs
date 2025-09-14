using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

using  SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddServiceDiscovery();
builder.Services.AddApiClient(builder.Configuration);
builder.Services.AddMudServices();



await builder.Build().RunAsync();