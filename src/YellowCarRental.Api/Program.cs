using SmartSolutionsLab.YellowCarRental.Application;
using SmartSolutionsLab.YellowCarRental.Application.Contracts;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Station;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Vehicle;
using SmartSolutionsLab.YellowCarRental.Domain;
using SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterAllApplicationCommandsAndHandlers();
builder.AddPersistence();
builder.AddServiceDefaults();

var app = builder.Build();

app.MapGet("/api/vehicles/search",
    (DateTime start, DateTime end, Guid? stationId, string? category,
            IQueryCommandHandler<SearchVehiclesQueryCommand, SearchVehiclesQueryResult> handler) =>
        handler.HandleQueryAsync(new SearchVehiclesQueryCommand(
            DateRange.From(start, end),
            StationIdentifier.IfPossibleOf(stationId),
            VehicleCategory.IfPossibleFromKey(category))));

app.MapGet("/api/stations", 
    (IQueryCommandHandler<ListAllStationsQueryCommand, ListStationsQueryResult> handler) =>
        handler.HandleQueryAsync(new ListAllStationsQueryCommand()));


app.MapGet("/api/bookings", 
    (IQueryCommandHandler<ListAllBookingsQueryCommand, SearchBookingsQueryResult> handler) =>
        handler.HandleQueryAsync(new ListAllBookingsQueryCommand()));

app.MapGet("/api/bookings/search",
    (DateTime start, DateTime end, Guid? stationId, Guid? customerId,
            IQueryCommandHandler<SearchBookingsQueryCommand, SearchBookingsQueryResult> handler) =>
        handler.HandleQueryAsync(new SearchBookingsQueryCommand(
            DateRange.From(start, end),
            StationIdentifier.IfPossibleOf(stationId),
            CustomerIdentifier.IfPossibleOf(customerId))));

app.MapPost("/api/bookings", async (BookVehicleCommand command, ICommandHandler<BookVehicleCommand, BookingIdentifier> handler, IBookings bookings) =>
{
    var bookingId = await handler.HandleAsync(command);
    var booking = (await bookings.FindById(bookingId)).ToData();
    
    return Results.Created($"/api/bookings/{bookingId}", booking);
});

app.MapGet("/api/customers/{id}", async (Guid id, IQueryCommandHandler<ShowCustomerCommand, CustomerData> handler, ICustomers customers) =>
{
    try
    {
        var customer = await handler.HandleQueryAsync(new ShowCustomerCommand(CustomerIdentifier.Of(id)));
        Results.Ok(customer);
    }
    catch (Exception)
    {
        Results.NotFound();
    }
});

app.MapPost("/api/customers", async (RegisterCustomerCommand command, ICommandHandler<RegisterCustomerCommand, CustomerIdentifier> handler, ICustomers customers) =>
{
    var customerId = await handler.HandleAsync(command);
    var customer = (await customers.FindById(customerId)).ToData();
    
    return Results.Created($"/api/customers/{customer.Id}", customer);
});



app.Run();

