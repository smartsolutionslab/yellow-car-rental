using System.Net.Http.Json;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;

namespace SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;

public class BookingApiClient(HttpClient http)
{
    public async Task<BookingData> BookVehicle(BookVehicleCommand command)
    {
        var response = await http.PostAsJsonAsync("/api/bookings", command);
        response.EnsureSuccessStatusCode();
        var createdBooking = await response.Content.ReadFromJsonAsync<BookingData>();
        
        return createdBooking ?? throw new InvalidOperationException("Failed to create booking.");
    }

    public async Task<SearchBookingsQueryResult> GetBookingsAsync()
    {
        return await http.GetFromJsonAsync<SearchBookingsQueryResult>("/api/bookings") ??
               SearchBookingsQueryResult.Empty;
    }
}