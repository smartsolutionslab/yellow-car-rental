using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Station;

namespace SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;

public class StationApiClient(HttpClient http, ILogger<StationApiClient> logger)
{
    public async Task<ListStationsQueryResult> ListStationsAsync(CancellationToken cancellationToken = default)
    {
        logger.BeginScope("Listing stations");
        
        var url = "/api/stations";
        var result = await http.GetFromJsonAsync<ListStationsQueryResult>(url, cancellationToken);
        return result ?? ListStationsQueryResult.Empty;
    }
}