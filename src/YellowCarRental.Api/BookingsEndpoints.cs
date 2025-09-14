using SmartSolutionsLab.YellowCarRental.Application;
using SmartSolutionsLab.YellowCarRental.Application.Contracts;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Api;

public static class BookingsEndpoints
{
    public static void MapBookingsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/bookings", 
            (IQueryCommandHandler<ListAllBookingsQueryCommand, SearchBookingsQueryResult> handler) =>
                handler.HandleQueryAsync(new ListAllBookingsQueryCommand()));

        app.MapGet("/api/bookings/search",
            (DateTime start, DateTime end, string? searchTerm,  Guid? stationId, Guid? customerId,
                    IQueryCommandHandler<SearchBookingsQueryCommand, SearchBookingsQueryResult> handler) =>
                handler.HandleQueryAsync(new SearchBookingsQueryCommand(
                    DateRange.From(start, end),
                    SearchTerm.IfPossibleOf(searchTerm),
                    StationIdentifier.IfPossibleOf(stationId),
                    CustomerIdentifier.IfPossibleOf(customerId))));

        app.MapGet("/api/bookings/customer/{customerId:guid}",  (Guid customerId, 
                IQueryCommandHandler<ShowBookingByCustomerQueryCommand, SearchBookingsQueryResult> handler) =>
            handler.HandleQueryAsync(new ShowBookingByCustomerQueryCommand(CustomerIdentifier.Of(customerId))));
    

        app.MapPost("/api/bookings", async (BookVehicleCommand command, ICommandHandler<BookVehicleCommand, BookingIdentifier> handler, IBookings bookings, IVehicles vehicles) =>
        {
            var bookingId = await handler.HandleAsync(command);
    
            var assignedVehicle = await vehicles.FindById(command.VehicleId);
            var booking = (await bookings.FindById(bookingId)).ToData(assignedVehicle);
    
            return Results.Created($"/api/bookings/{bookingId}", booking);
        });

        app.MapPost("/api/bookings/{id:guid}/cancel", async (Guid id, ICommandHandler<CancelBookingCommand, BookingIdentifier> handler) =>
        {
            try
            {
                await handler.HandleAsync(new CancelBookingCommand(BookingIdentifier.Of(id)));
                Results.Ok("Booking cancelled");
            }
            catch (Exception)
            {
                Results.BadRequest();
            }
        });
    }
}