using Microsoft.Extensions.DependencyInjection;

namespace SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;

public static class Extensions
{
    public static IServiceCollection AddApiClient(this IServiceCollection services)
    {
        services.AddHttpClient<VehicleApiClient>(client => { 
            client.BaseAddress =  new Uri("https+http://api");
        });
        
        services.AddHttpClient<StationApiClient>(client => { 
            client.BaseAddress =  new Uri("https+http://api");
        });
        
        services.AddHttpClient<BookingApiClient>(client => { 
            client.BaseAddress =  new Uri("https+http://api");
        });
        
        services.AddHttpClient<CustomerApiClient>(client => { 
            client.BaseAddress =  new Uri("https+http://api");
        });

        return services;
    }
}