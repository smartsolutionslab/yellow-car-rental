using System.Net.Http.Json;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Vehicle;

namespace SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;

public class VehicleApiClient(HttpClient http)
{
    public async Task<SearchVehiclesQueryResult> SearchVehiclesAsync(DateTime start, DateTime end, Guid? station, string? category)
    {
        var url = $"/api/vehicles/search?start={start:O}&end={end:O}&stationId={station}&category={category}";
        var result = await http.GetFromJsonAsync<SearchVehiclesQueryResult>(url);
        return result ?? SearchVehiclesQueryResult.Empty;
    }
}