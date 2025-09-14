using SmartSolutionsLab.YellowCarRental.Application.Contracts;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application;

public class BookingCommandHandlers(IBookings bookings, IVehicles vehicles, IStations stations) :
    IQueryCommandHandler<ListAllBookingsQueryCommand, SearchBookingsQueryResult>,
    IQueryCommandHandler<SearchBookingsQueryCommand, SearchBookingsQueryResult>,
    IQueryCommandHandler<CheckBookingAvailabilityQueryCommand, SearchBookingsQueryResult>,
    ICommandHandler<BookVehicleCommand, BookingIdentifier>,
    ICommandHandler<CancelBookingCommand, BookingIdentifier>
{
    public async Task<SearchBookingsQueryResult> HandleQueryAsync(SearchBookingsQueryCommand command)
    {
        var (period, stationId, customerId) = command;

        var foundBookings = await bookings.With(period, stationId, customerId);
        var relatedVehicles = await RelatedVehicles(foundBookings);
        
        return new SearchBookingsQueryResult([..foundBookings.ToData(relatedVehicles)]);
    }
    
    public async Task<SearchBookingsQueryResult> HandleQueryAsync(ListAllBookingsQueryCommand command)
    {
        var allBookings = await bookings.All();
        var relatedVehicles = await RelatedVehicles(allBookings);

        return new SearchBookingsQueryResult([..(allBookings.ToData(relatedVehicles))]);
    }

    public async Task<BookingIdentifier> HandleAsync(BookVehicleCommand command)
    {
        var(vehicleId, customerData, pickupStationId, returnStationId, period) = command;
        
        _ = await vehicles.FindById(vehicleId)
                      ?? throw new Exception("Vehicle not found");
        
        var pickupStation = await stations.FindById(pickupStationId)
                            ?? throw new Exception("Pickup station not found");

        var returnStation = await stations.FindById(returnStationId)
                            ?? throw new Exception("Return station not found");

        if (!pickupStation.HasVehicleAvailable(vehicleId))
        {
            throw new Exception("This Vehicle is not available at the selected pickup station");
        }

        if (period.Start >= period.End)
        {
            throw new Exception("Invalid period");
        }
        
        //TODO: add checks for vehicle availability, customer validity, station validity etc.
        
        var totalPrice = await CalculateTotalPrice(vehicleId, period);

        var booking = Booking.From(vehicleId, customerData, period, pickupStationId, returnStationId, totalPrice);

        pickupStation.RemoveVehicle(vehicleId);
        returnStation.AssignVehicle(vehicleId);
        
        await bookings.Add(booking);
        await stations.Update(pickupStation);
        await stations.Update(returnStation);

        async Task<Money> CalculateTotalPrice(VehicleIdentifier vehicleIdentifier, DateRange dateRange)
        {
            var selectedVehicle = await vehicles.FindById(vehicleIdentifier);
            var pricePerDay = selectedVehicle.PricePerDay;

            var money = Money.Of(pricePerDay.Amount * dateRange.TotalDaysInclusive(), pricePerDay.Currency);
            return money;
        }

        return booking.Id;
    }

    public async Task<SearchBookingsQueryResult> HandleQueryAsync(CheckBookingAvailabilityQueryCommand command)
    {
        var (vehicleId, period) = command;
        
        var foundBookings = await bookings.ForVehicle(vehicleId, period);
        var relatedVehicles = await RelatedVehicles(foundBookings);

        return new(foundBookings.ToData(relatedVehicles).ToList());
    }

    private async Task<Dictionary<VehicleIdentifier, Vehicle>> RelatedVehicles(IList<Booking> bookingsList)
    {
        var usedVehicleIds = bookingsList.Select(booking => booking.VehicleId).Distinct().ToList();
        var relatedVehicles = (await vehicles.FindAll(usedVehicleIds)).ToDictionary(vehicle => vehicle.Id);
        return relatedVehicles;
    }

    public async Task<BookingIdentifier> HandleAsync(CancelBookingCommand command)
    {
        var bookingId = command.BookingId;
        var booking = await bookings.FindById(bookingId) ?? throw new Exception("Booking not found");
        
        booking.Cancel();
        
        var pickupStation = await stations.FindById(booking.PickupStationId);
        var returnStation = await stations.FindById(booking.ReturnStationId);
        
        
        // in future
        if(DateTime.Now < booking.Period.Start.ToDateTime(TimeOnly.MinValue))
        {
            returnStation.RemoveVehicle(booking.VehicleId); // hopefully in this order no problem with pickup and return station are the same
            pickupStation.AssignVehicle(booking.VehicleId);
            
        }
        else
        {
            // If the cancellation is done during or after the rental period, the vehicle should be returned to the return station
            returnStation.AssignVehicle(booking.VehicleId);
        }
        
        await bookings.Update(booking);

        return bookingId;
    }
}