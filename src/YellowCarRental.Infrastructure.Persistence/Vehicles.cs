using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public class Vehicles : IVehicles
{
    public static readonly Vehicle Bmw320DBlau = Vehicle.From("BMW 320d touring blau", VehicleCategory.Sedan, FuelType.Diesel, TransmissionType.Automatic, 80);
    public static readonly Vehicle Bmw320DRot = Vehicle.From("BMW 320d touring rot", VehicleCategory.Sedan, FuelType.Diesel, TransmissionType.Automatic, 80);
    public static readonly Vehicle Bmw320DSchwarz = Vehicle.From("BMW 320d touring schwarz", VehicleCategory.Sedan, FuelType.Diesel, TransmissionType.Automatic, 80);
    public static readonly Vehicle Bmw320DWeiss = Vehicle.From("BMW 320d touring weiss", VehicleCategory.Sedan, FuelType.Diesel, TransmissionType.Automatic, 80);
    public static readonly Vehicle Bmw320DGruen = Vehicle.From("BMW 320d touring grün", VehicleCategory.Sedan, FuelType.Diesel, TransmissionType.Automatic, 80);
    
    public static readonly Vehicle Bmw118IWeiss = Vehicle.From("BMW 118i weiss", VehicleCategory.Sedan, FuelType.Petrol, TransmissionType.Automatic, 60);
    public static readonly Vehicle Bmw118IBlau = Vehicle.From("BMW 118i blau", VehicleCategory.Sedan, FuelType.Petrol, TransmissionType.Automatic, 60);
    public static readonly Vehicle Bmw118ISilber = Vehicle.From("BMW 118i silber", VehicleCategory.Sedan, FuelType.Petrol, TransmissionType.Automatic, 60);
    public static readonly Vehicle Bmw118IRot = Vehicle.From("BMW 118i rot", VehicleCategory.Sedan, FuelType.Petrol, TransmissionType.Automatic, 60);
    public static readonly Vehicle Bmw118ISchwarz = Vehicle.From("BMW 118i schwarz", VehicleCategory.Sedan, FuelType.Petrol, TransmissionType.Automatic, 60);

    public static readonly Vehicle VwGolfWeiss = Vehicle.From("VW Golf weiss", VehicleCategory.Sedan, FuelType.Petrol, TransmissionType.Manual, 40);
    public static readonly Vehicle VwGolfRot = Vehicle.From("VW Golf rot", VehicleCategory.Sedan, FuelType.Petrol, TransmissionType.Manual, 40);
    public static readonly Vehicle VwGolfSchwarz = Vehicle.From("VW Golf schwarz", VehicleCategory.Sedan, FuelType.Petrol, TransmissionType.Manual, 40);
    public static readonly Vehicle VwGolfSilber = Vehicle.From("VW Golf silber", VehicleCategory.Sedan, FuelType.Petrol, TransmissionType.Manual, 40);
    public static readonly Vehicle VwGolfGelb = Vehicle.From("VW Golf  gelb", VehicleCategory.Sedan, FuelType.Petrol, TransmissionType.Manual, 40);
    
    public static readonly Vehicle AudiQ5Schwarz = Vehicle.From("Audi Q5 schwarz", VehicleCategory.Suv, FuelType.Petrol, TransmissionType.Automatic, 90);
    public static readonly Vehicle AudiQ5Gruen = Vehicle.From("Audi Q5 gruen", VehicleCategory.Suv, FuelType.Petrol, TransmissionType.Automatic, 90);
    public static readonly Vehicle AudiQ5Grau = Vehicle.From("Audi Q5 Grau", VehicleCategory.Suv, FuelType.Petrol, TransmissionType.Automatic, 90);
    public static readonly Vehicle AudiQ5Weiss = Vehicle.From("Audi Q5 Weiss", VehicleCategory.Suv, FuelType.Petrol, TransmissionType.Automatic, 90);
    public static readonly Vehicle AudiQ5Blau = Vehicle.From("Audi Q5 Blau", VehicleCategory.Suv, FuelType.Petrol, TransmissionType.Automatic, 90);
    
    public static readonly Vehicle MercedesGlaSilber = Vehicle.From("Mercedes GLA silber", VehicleCategory.Suv, FuelType.Diesel, TransmissionType.Automatic, 70);
    public static readonly Vehicle MercedesGlaRot = Vehicle.From("Mercedes GLA Rot", VehicleCategory.Suv, FuelType.Diesel, TransmissionType.Automatic, 70);
    public static readonly Vehicle MercedesGlaBlau = Vehicle.From("Mercedes GLA Blau", VehicleCategory.Suv, FuelType.Diesel, TransmissionType.Automatic, 70);
    public static readonly Vehicle MercedesGlaWeiss = Vehicle.From("Mercedes GLA Weiss", VehicleCategory.Suv, FuelType.Diesel, TransmissionType.Automatic, 70);
    public static readonly Vehicle MercedesGlaHellblau = Vehicle.From("Mercedes GLA Hellblau", VehicleCategory.Suv, FuelType.Diesel, TransmissionType.Automatic, 70);
    
    
    private IList<Vehicle> _vehicles = [
        Bmw320DBlau, Bmw320DRot, Bmw320DSchwarz, Bmw320DWeiss, Bmw320DGruen,
        Bmw118IWeiss, Bmw118IBlau, Bmw118ISilber, Bmw118IRot, Bmw118ISchwarz,
        VwGolfWeiss, VwGolfRot, VwGolfSchwarz, VwGolfSilber, VwGolfGelb,
        AudiQ5Schwarz, AudiQ5Gruen, AudiQ5Grau, AudiQ5Weiss, AudiQ5Blau,
        MercedesGlaSilber, MercedesGlaRot, MercedesGlaBlau, MercedesGlaWeiss, MercedesGlaHellblau];
    
    public Task<IList<Vehicle>> With(DateRange period, StationIdentifier? stationId = null, VehicleCategory? category = null)
    {
        throw new NotImplementedException();
        //var query = _vehicles.Where(vehicle => vehicle.)
    }

    public Task<Vehicle> FindById(VehicleIdentifier vehicleId)
    {
        return Task.FromResult(_vehicles.SingleOrDefault(v => v.Id == vehicleId) ?? throw new Exception($"Vehicle with ID {vehicleId} not found"));
    }

    public async Task<IList<Vehicle>> FindAll(IEnumerable<VehicleIdentifier> requestedVehicleIds)
    {
        await Task.CompletedTask;
        
        return _vehicles.ToList().FindAll(vehicle => requestedVehicleIds.Contains(vehicle.Id));
    }
}