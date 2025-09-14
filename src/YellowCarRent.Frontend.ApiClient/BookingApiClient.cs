using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;

namespace SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;

public class BookingApiClient(HttpClient http, ILogger<BookingApiClient> logger)
{
    public async Task<BookingData> BookVehicle(BookVehicleCommand command)
    {
        logger.LogDebug("I was here" + command.Customer + "http " + http.BaseAddress);
        
        var response = await http.PostAsJsonAsync("/api/bookings", command);
        response.EnsureSuccessStatusCode();
        var createdBooking = await response.Content.ReadFromJsonAsync<BookingData>();
        
        return createdBooking ?? throw new InvalidOperationException("Failed to create booking.");
    }

    public async Task<SearchBookingsQueryResult> GetBookingsAsync()
    {
        logger.LogDebug("I was here"  + "http " + http.BaseAddress);
        
        return await http.GetFromJsonAsync<SearchBookingsQueryResult>("/api/bookings") ??
               SearchBookingsQueryResult.Empty;
    }
    
    public async Task<SearchBookingsQueryResult> GetBookingsForCustomerAsync(Guid customerId)
    {
        logger.LogDebug("I was here Customer Id" + customerId + "http " + http.BaseAddress);
        
        return await http.GetFromJsonAsync<SearchBookingsQueryResult>($"api/booking/customer/{customerId}") //TODO : check route
               ?? SearchBookingsQueryResult.Empty;
    }
    
    public async Task CancelBookingAsync(Guid bookingId)
    {
        var response = await http.PostAsync($"api/booking/{bookingId}/cancel", null);
        response.EnsureSuccessStatusCode();
    }
}