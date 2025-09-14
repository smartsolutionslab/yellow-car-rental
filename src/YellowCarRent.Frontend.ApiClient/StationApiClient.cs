using System.Net.Http.Json;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Station;

namespace SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;

public class StationApiClient(HttpClient http)
{
    public async Task<ListStationsQueryResult> ListStationsAsync()
    {
        var url = "/api/stations";
        var result = await http.GetFromJsonAsync<ListStationsQueryResult>(url);
        return result ?? ListStationsQueryResult.Empty;
    }
}