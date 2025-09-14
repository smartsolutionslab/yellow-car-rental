using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;

public static class Extensions
{
    public static WebAssemblyHostBuilder AddApiClient(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddHttpClient<VehicleApiClient>(client => { 
            client.BaseAddress =  new Uri("https+http://yellowcarrental-api");
        });
        builder.Services.AddScoped<VehicleApiClient>();
        
        builder.Services.AddHttpClient<StationApiClient>(client => { 
            client.BaseAddress =  new Uri("https+http://yellowcarrental-api");
        });
        builder.Services.AddScoped<StationApiClient>();
        
        builder.Services.AddHttpClient<BookingApiClient>(client => { 
            client.BaseAddress =  new Uri("https+http://yellowcarrental-api");
        });
        builder.Services.AddScoped<BookingApiClient>();
        
        builder.Services.AddHttpClient<CustomerApiClient>(client => { 
            client.BaseAddress =  new Uri("https+http://yellowcarrental-api");
        });
        builder.Services.AddScoped<CustomerApiClient>();

        return builder;
    }
}