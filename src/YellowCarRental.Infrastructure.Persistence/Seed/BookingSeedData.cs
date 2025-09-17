using SmartSolutionsLab.YellowCarRental.Domain;

internal static class BookingSeedData
{
    private static IReadOnlyList<Booking>? _cache;

    /// <summary>
    /// Generates deterministic booking data (cached) for testing.
    /// Assumptions:
    ///  - Booking factory: Booking.From(Customer customer, VehicleIdentifier vehicleId, StationIdentifier pickupStationId, StationIdentifier returnStationId, DateRange period)
    ///  - Date range factory: DateRange.From(DateOnly start, DateOnly end)
    /// Adjust the construction if your actual APIs differ.
    /// </summary>
    public static IReadOnlyList<Booking> GetAll(
        IReadOnlyList<Customer> customers,
        IReadOnlyList<Station> stations,
        IReadOnlyList<VehicleIdentifier> vehicles,
        int count = 200,
        int seed = 2024)
    {
        if (_cache is not null && _cache.Count == count)
            return _cache;

        var stationsList = stations.ToList();
        
        if (customers.Count == 0) throw new ArgumentException("Customers required", nameof(customers));
        if (stationsList.Count < 2) throw new ArgumentException("At least two stations required", nameof(stationsList));
        if (vehicles.Count == 0) throw new ArgumentException("Vehicles required", nameof(vehicles));

        var rnd = new Random(seed);
        var list = new List<Booking>(count);

        // Base window: 30 days past to 90 days future
        var today = DateOnly.FromDateTime(DateTime.Today);

        for (int i = 0; i < count; i++)
        {
            var customer = customers[i % customers.Count];
            var vehicleId = vehicles[i % vehicles.Count];

            // Random stations (ensure sometimes different pickup/return)
            var pickupStation = stationsList[rnd.Next(stationsList.Count)];
            

            // Temporal distribution
            int startOffset = rnd.Next(-90, 90);          // days from today
            int durationDays = rnd.Next(1, 14);           // 1..14 days
            var start = today.AddDays(startOffset);
            var end = start.AddDays(durationDays);
            var pricePerDay = rnd.Next(25, 120);

            var period = DateRange.From(start, end);      // Adjust if constructor differs

            // Create booking (adjust if your signature differs)
            var booking = Booking.From(
                vehicleId,
                customer,
                period,
                pickupStation.Id,
                pickupStation.Id,
                pricePerDay: Money.Of(pricePerDay, "EUR") // Assume flat rate; adjust as needed
            );
            
            if(booking.Period.End < today && i % 5 == 0)
            {
                // Complete some past bookings
                booking.Complete();
            }
            else if (booking.Period.Start < today && i % 7 == 0)
            {
                // Cancel some current/past bookings
                booking.Cancel();
            }

            list.Add(booking);
        }

        _cache = list.AsReadOnly();
        return _cache;
    }
}