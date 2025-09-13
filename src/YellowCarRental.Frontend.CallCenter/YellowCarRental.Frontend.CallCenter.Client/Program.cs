using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

using  SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.AddApiClient();

builder.Services.AddMudServices();

await builder.Build().RunAsync();