using SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Station;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Vehicle;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Application;

public static class MappingExtensions
{
    public static IEnumerable<VehicleData> ToData(this IEnumerable<Vehicle> vehicles)
    {
        return vehicles.Select(vehicle => new VehicleData
        {
            Id = vehicle.Id.Value,
            Name = vehicle.Name.Value,
            Category = vehicle.Category.Key,
            Seats = vehicle.Seats,
            Fuel = vehicle.Fuel.Key,
            Transmission = vehicle.Transmission.Key,
            PricePerDay = vehicle.PricePerDay.Amount,
        });
    }

    public static IEnumerable<StationData> ToData(this IEnumerable<Station> stations)
    {
        return stations.Select(station => new StationData
        {
            Id = station.Id.Value,
            Name = station.Name.Value
        });
    }
    public static IEnumerable<BookingData> ToData(this IEnumerable<Booking> bookings)
    {
        return bookings.Select(booking => booking.ToData());
    }
    
    public static BookingData ToData(this Booking booking)
    {
        return new BookingData
        {
            Id = booking.Id.Value,
            VehicleId = booking.VehicleId.Value,
            PickupStationId = booking.PickupStationId.Value,
            ReturnStationId = booking.ReturnStationId.Value,
            Customer = new BookingCustomerData
            {
                Id = booking.Customer.Id.Value,
                FirstName = booking.Customer.Name.FirstName.Value,
                LastName = booking.Customer.Name.LastName.Value,
                Email = booking.Customer.Email.Value,
            },
            StartDate = booking.Period.Start.ToDateTime(TimeOnly.MinValue),
            EndDate = booking.Period.End.ToDateTime(TimeOnly.MinValue),
            TotalPrice = booking.TotalPrice.Amount,
            TotalPriceCurrency = booking.TotalPrice.Currency,
        };
    }

    public static CustomerData ToData(this Customer customer)
    {
        return new CustomerData
        {
            Id = customer.Id.Value,
            Salutation = customer.Name.Salutation.Value,
            FirstName = customer.Name.FirstName.Value,
            LastName = customer.Name.LastName.Value,
            BirthDate = customer.BirthDate.Value.ToDateTime(TimeOnly.MinValue),
            Street = customer.Address.Street.Value,
            HouseNumber = customer.Address.HouseNumber.Value,
            Zip = customer.Address.ZipCode.Value,
            City = customer.Address.City.Value,
        };
    }
    
    public static IEnumerable<CustomerData> ToData(this IEnumerable<Customer> customers)
    {
        return customers.Select(customer => customer.ToData());
    }
}