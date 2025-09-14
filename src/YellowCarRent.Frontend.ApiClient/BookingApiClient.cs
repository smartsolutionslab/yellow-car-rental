using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;

public class BookingApiClient(HttpClient http, ILogger<BookingApiClient> logger)
{
    public async Task<BookingData> BookVehicle(BookVehicleCommand command, CancellationToken cancellationToken = default)
    {
        logger.BeginScope("Booking vehicle {VehicleId} for customer {CustomerName}", command.VehicleId, command.Customer.Name);
        
        var response = await http.PostAsJsonAsync("/api/bookings", command, cancellationToken);
        response.EnsureSuccessStatusCode();
        var createdBooking = await response.Content.ReadFromJsonAsync<BookingData>(cancellationToken);

        return createdBooking ?? throw new ApiClientException("Failed to create booking.");
    }

    public async Task<SearchBookingsQueryResult> GetBookingsAsync(CancellationToken cancellationToken = default)
    {
        logger.BeginScope("Getting all bookings");
        
        return await http.GetFromJsonAsync<SearchBookingsQueryResult>("/api/bookings", cancellationToken) ??
               SearchBookingsQueryResult.Empty;
    } 
    public async Task<SearchBookingsQueryResult> SearchBookingsByAsync(
        DateRange period, 
        SearchTerm? searchTerm,
        StationIdentifier? stationId, 
        CustomerIdentifier? customerId,
        CancellationToken cancellationToken = default)
    {
        
        logger.BeginScope("Searching bookings by period {Start} - {End}, station {StationId}, customer {CustomerId}", period.Start, period.End, stationId, customerId);
        
        var url = $"/api/bookings/search?start={period.Start:O}&end={period.End:O}";
        if(searchTerm is not null)
            url += $"&searchTerm={Uri.EscapeDataString(searchTerm.Value)}";
        if (stationId is not null)
            url += $"&stationId={stationId.Value}";
        if (customerId is not null)
            url += $"&customerId={customerId.Value}";   
        
        return await http.GetFromJsonAsync<SearchBookingsQueryResult>(url, cancellationToken) ??
               SearchBookingsQueryResult.Empty;
    }
    
    public async Task<SearchBookingsQueryResult> GetBookingsForCustomerAsync(
        CustomerIdentifier customerId, 
        CancellationToken cancellationToken = default)
    {
        logger.BeginScope("Getting all bookings for customer {CustomerId}", customerId);
        
        return await http.GetFromJsonAsync<SearchBookingsQueryResult>($"api/bookings/customer/{customerId.Value}", cancellationToken)
               ?? SearchBookingsQueryResult.Empty;
    }
    
    public async Task CancelBookingAsync(
        BookingIdentifier bookingId, 
        CancellationToken cancellationToken = default)
    {
        logger.BeginScope("Cancelling booking {BookingId}", bookingId);
        
        var response = await http.PostAsync($"api/bookings/{bookingId.Value}/cancel", null, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}