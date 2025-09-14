using SmartSolutionsLab.YellowCarRental.Application.Contracts;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application;

public class BookingCommandHandlers(IBookings bookings, IVehicles vehicles, IStations stations) :
    IQueryCommandHandler<ListAllBookingsQueryCommand, SearchBookingsQueryResult>,
    IQueryCommandHandler<SearchBookingsQueryCommand, SearchBookingsQueryResult>,
    IQueryCommandHandler<CheckBookingAvailabilityQueryCommand, SearchBookingsQueryResult>,
    IQueryCommandHandler<ShowBookingByCustomerQueryCommand, SearchBookingsQueryResult>,
    ICommandHandler<BookVehicleCommand, BookingIdentifier>,
    ICommandHandler<CancelBookingCommand, BookingIdentifier>
{
    public async Task<SearchBookingsQueryResult> HandleQueryAsync(SearchBookingsQueryCommand command)
    {
        var (period, searchTerm, stationId, customerId) = command;

        var foundBookings = await bookings.With(period, searchTerm, stationId, customerId);
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
                      ?? throw new ApplicationException("Vehicle not found");
        
        var pickupStation = await stations.FindById(pickupStationId)
                            ?? throw new ApplicationException("Pickup station not found");

        var returnStation = await stations.FindById(returnStationId)
                            ?? throw new ApplicationException("Return station not found");

        if (!pickupStation.HasVehicleAvailable(vehicleId))
        {
            throw new ApplicationException("This Vehicle is not available at the selected pickup station");
        }

        if (period.Start >= period.End)
        {
            throw new ApplicationException("Invalid period");
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
    
    public async Task<SearchBookingsQueryResult> HandleQueryAsync(ShowBookingByCustomerQueryCommand command)
    {
        var customerId = command.CustomerId;

        //TODO: repo is here reused.
        var foundBookings = await bookings.With(null, null,null, customerId);
        var relatedVehicles = await RelatedVehicles(foundBookings);

        return new(foundBookings.ToData(relatedVehicles).ToList());
    }

    public async Task<SearchBookingsQueryResult> HandleQueryAsync(CheckBookingAvailabilityQueryCommand command)
    {
        var (vehicleId, period) = command;
        
        var foundBookings = await bookings.ForVehicle(vehicleId, period);
        var relatedVehicles = await RelatedVehicles(foundBookings);

        return new(foundBookings.ToData(relatedVehicles).ToList());
    }

    private async Task<Dictionary<VehicleIdentifier, Vehicle>> RelatedVehicles(IEnumerable<Booking> bookingsList)
    {
        var usedVehicleIds = bookingsList.Select(booking => booking.VehicleId).Distinct().ToList();
        var relatedVehicles = (await vehicles.FindAll(usedVehicleIds)).ToDictionary(vehicle => vehicle.Id);
        return relatedVehicles;
    }

    public async Task<BookingIdentifier> HandleAsync(CancelBookingCommand command)
    {
        var bookingId = command.BookingId;
        var booking = await bookings.FindById(bookingId) ?? throw new ApplicationException("Booking not found");
        
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