using SmartSolutionsLab.YellowCarRental.Application.Contracts;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application;

public class BookingCommandHandlers(IBookings bookings, IVehicles vehicles) :
    IQueryCommandHandler<ListAllBookingsQueryCommand, SearchBookingsQueryResult>,
    IQueryCommandHandler<SearchBookingsQueryCommand, SearchBookingsQueryResult>,
    ICommandHandler<BookVehicleCommand, BookingIdentifier>
{
    public async Task<SearchBookingsQueryResult> HandleQueryAsync(SearchBookingsQueryCommand command)
    {
        var (period, stationId, customerId) = command;

        return new SearchBookingsQueryResult([..(await bookings.With(period, stationId, customerId)).ToData()]);
    }
    
    public async Task<SearchBookingsQueryResult> HandleQueryAsync(ListAllBookingsQueryCommand command)
    {
        return new SearchBookingsQueryResult([..(await bookings.All()).ToData()]);
    }

    public async Task<BookingIdentifier> HandleAsync(BookVehicleCommand command)
    {
        var(vehicleId, customerData, pickupStationId, returnStationId, period) = command;
        
        
        //TODO: add checks for vehicle availability, customer validity, station validity etc.
        
        var totalPrice = await CalculateTotalPrice(vehicleId, period);

        var booking = Booking.From(vehicleId, customerData, period, pickupStationId, returnStationId, totalPrice);

        await bookings.Add(booking);

        async Task<Money> CalculateTotalPrice(VehicleIdentifier vehicleIdentifier, DateRange dateRange)
        {
            var selectedVehicle = await vehicles.FindById(vehicleIdentifier);
            var pricePerDay = selectedVehicle.PricePerDay;

            var money = Money.Of(pricePerDay.Amount * dateRange.TotalDaysInclusive(), pricePerDay.Currency);
            return money;
        }

        return booking.Id;
    }
}