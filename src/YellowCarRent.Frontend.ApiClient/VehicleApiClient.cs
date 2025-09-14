using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Vehicle;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;

public class VehicleApiClient(HttpClient http, ILogger<VehicleApiClient> logger)
{
    public async Task<SearchVehiclesQueryResult> SearchVehiclesAsync(
        DateRange period, 
        StationIdentifier? stationId, 
        VehicleCategory? category, 
        CancellationToken cancellationToken = default)
    {
        logger.BeginScope("Searching vehicles");
        
        var url = $"/api/vehicles/search?start={period.Start:O}&end={period.End:O}";

        if (stationId is not null)
            url += $"&stationId={stationId.Value}";
        
        if(category is not null)
            url += $"&category={category.Key}";
        
        var result = await http.GetFromJsonAsync<SearchVehiclesQueryResult>(url, cancellationToken);
        return result ?? SearchVehiclesQueryResult.Empty;
    }
    
    public async Task<VehicleData> FindVehicleByIdAsync(
        VehicleIdentifier vehicleId, 
        CancellationToken cancellationToken = default)
    {
        logger.BeginScope("Finding vehicle by ID");
        
        var url = $"/api/vehicles/{vehicleId.Value}";
        var vehicle = await http.GetFromJsonAsync<VehicleData>(url, cancellationToken) ;
        
        return vehicle ?? throw new ApiClientException($"Vehicle with ID {vehicleId.Value} not found.");
    }
    
    public async Task<SearchSimilarVehiclesQueryResult> ShowSimilarVehiclesAsync(
        VehicleIdentifier vehicleId, 
        DateRange period, StationIdentifier? station, 
        CancellationToken cancellationToken = default)
    {
        logger.BeginScope("Showing similar vehicles");   
        
        var url = $"/api/vehicles/{vehicleId.Value}/similar?start={period.Start:O}&end={period.End:O}";
        
        if(station is not null)
            url+= $"&stationId={station.Value}";
        
        var result = await http.GetFromJsonAsync<SearchSimilarVehiclesQueryResult>(url, cancellationToken);
        return result ?? SearchSimilarVehiclesQueryResult.Empty;
    }
}