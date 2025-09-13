using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;

public static class Extensions
{
    public static WebAssemblyHostBuilder AddApiClient(this WebAssemblyHostBuilder builder)
    {
        var baseUrl = builder.Configuration["ApiBaseUrl"];
        
        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            baseUrl = "https://localhost:5001"; // fallback for local dev
        }

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseUrl) });
        builder.Services.AddScoped<VehicleApiClient>();

        return builder;
    }
}