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
        return vehicles.Select(vehicle => vehicle.ToData());
    }
    
    public static VehicleData ToData(this Vehicle vehicle)
    {
        return new VehicleData
        {
            Id = vehicle.Id.Value,
            Name = vehicle.Name.Value,
            Category = vehicle.Category.Key,
            Fuel = vehicle.Fuel.Key,
            Transmission = vehicle.Transmission.Key,
            PricePerDay = vehicle.PricePerDay.Amount,
            StationId = vehicle.StationId.Value
        };
    }

    public static IEnumerable<StationData> ToData(this IEnumerable<Station> stations)
    {
        return stations.Select(station => new StationData
        {
            Id = station.Id.Value,
            Name = station.Name.Value
        });
    }
    public static IEnumerable<BookingData> ToData(this IEnumerable<Booking> bookings, IDictionary<VehicleIdentifier, Vehicle> relatedVehicles)
    {
        return bookings.Select(booking => booking.ToData( relatedVehicles[booking.VehicleId]));
    }
    
    public static BookingData ToData(this Booking booking, Vehicle vehicle)
    {
        return new BookingData
        {
            Id = booking.Id.Value,
            VehicleId = booking.VehicleId.Value,
            VehicleName = booking.VehicleId == vehicle.Id ? vehicle.Name.Value : "Unknown Vehicle",
            PickupStationId = booking.PickupStationId.Value,
            ReturnStationId = booking.ReturnStationId.Value,
            Customer = new BookingCustomerData
            {
                Id = booking.Customer.Id.Value,
                Salutation = booking.Customer.Name.Salutation.Value,
                FirstName = booking.Customer.Name.FirstName.Value,
                LastName = booking.Customer.Name.LastName.Value,
                BirthDate = booking.Customer.BirthDate.Value,
            },
            StartDate = booking.Period.Start.ToDateTime(TimeOnly.MinValue),
            EndDate = booking.Period.End.ToDateTime(TimeOnly.MinValue),
            TotalPrice = booking.TotalPrice.Amount,
            TotalPriceCurrency = booking.TotalPrice.Currency,
            Status = Enum.GetName(booking.Status) ?? string.Empty
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
            Email = customer.EMail.Value,
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