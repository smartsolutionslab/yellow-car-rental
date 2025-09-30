using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;

public static class Extensions
{
    public static IServiceCollection AddApiClient(this IServiceCollection services, IConfiguration? configuration)
    {
        //var baseAddress = new Uri(configuration?["ApiBaseUrl"] ?? "https+http://api");
        var baseAddress = new Uri("https://localhost:7207"); // Workaround for dev

        services.AddHttpClient<VehicleApiClient>(client =>
        {
            client.BaseAddress = baseAddress;
        });
        
        services.AddHttpClient<StationApiClient>(client =>
        {
            client.BaseAddress = baseAddress;
        });
        
        services.AddHttpClient<BookingApiClient>(client =>
        {
            client.BaseAddress = baseAddress;
        });
        
        services.AddHttpClient<CustomerApiClient>(client =>
        {
            client.BaseAddress = baseAddress;
        });

        return services;
    }
}