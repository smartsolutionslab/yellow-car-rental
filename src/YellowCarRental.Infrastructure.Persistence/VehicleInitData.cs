using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public class VehicleInitData
{
    public static IEnumerable<Vehicle> AllVehicles =>
    [
        Bmw320DBlau, Bmw320DRot, Bmw320DSchwarz, Bmw320DWeiss, Bmw320DGruen,
        Bmw118IWeiss, Bmw118IBlau, Bmw118ISilber, Bmw118IRot, Bmw118ISchwarz,
        VwGolfWeiss, VwGolfRot, VwGolfSchwarz, VwGolfSilber, VwGolfGelb,
        AudiQ5Schwarz, AudiQ5Gruen, AudiQ5Grau, AudiQ5Weiss, AudiQ5Blau,
        MercedesGlaSilber, MercedesGlaRot, MercedesGlaBlau, MercedesGlaWeiss, MercedesGlaHellblau
    ];
    
    public static readonly Vehicle Bmw320DBlau = Vehicle.From("BMW 320d touring blau", VehicleCategory.Sedan,
        FuelType.Diesel, TransmissionType.Automatic, 80, StationsInitData.Berlin.Id);

    public static readonly Vehicle Bmw320DRot = Vehicle.From("BMW 320d touring rot", VehicleCategory.Sedan,
        FuelType.Diesel, TransmissionType.Automatic, 80, StationsInitData.Frankfurt.Id);

    public static readonly Vehicle Bmw320DSchwarz = Vehicle.From("BMW 320d touring schwarz", VehicleCategory.Sedan,
        FuelType.Diesel, TransmissionType.Automatic, 80, StationsInitData.Hamburg.Id);

    public static readonly Vehicle Bmw320DWeiss = Vehicle.From("BMW 320d touring weiss", VehicleCategory.Sedan,
        FuelType.Diesel, TransmissionType.Automatic, 80, StationsInitData.Muenchen.Id);

    public static readonly Vehicle Bmw320DGruen = Vehicle.From("BMW 320d touring grün", VehicleCategory.Sedan,
        FuelType.Diesel, TransmissionType.Automatic, 80, StationsInitData.Stuttgart.Id);

    public static readonly Vehicle Bmw118IWeiss = Vehicle.From("BMW 118i weiss", VehicleCategory.Sedan, FuelType.Petrol,
        TransmissionType.Automatic, 60, StationsInitData.Berlin.Id);

    public static readonly Vehicle Bmw118IBlau = Vehicle.From("BMW 118i blau", VehicleCategory.Sedan, FuelType.Petrol,
        TransmissionType.Automatic, 60, StationsInitData.Frankfurt.Id);

    public static readonly Vehicle Bmw118ISilber = Vehicle.From("BMW 118i silber", VehicleCategory.Sedan,
        FuelType.Petrol, TransmissionType.Automatic, 60, StationsInitData.Hamburg.Id);

    public static readonly Vehicle Bmw118IRot = Vehicle.From("BMW 118i rot", VehicleCategory.Sedan, FuelType.Petrol,
        TransmissionType.Automatic, 60, StationsInitData.Muenchen.Id);

    public static readonly Vehicle Bmw118ISchwarz = Vehicle.From("BMW 118i schwarz", VehicleCategory.Sedan,
        FuelType.Petrol, TransmissionType.Automatic, 60, StationsInitData.Stuttgart.Id);

    public static readonly Vehicle VwGolfWeiss = Vehicle.From("VW Golf weiss", VehicleCategory.Sedan, FuelType.Petrol,
        TransmissionType.Manual, 40, StationsInitData.Berlin.Id);

    public static readonly Vehicle VwGolfRot = Vehicle.From("VW Golf rot", VehicleCategory.Sedan, FuelType.Petrol,
        TransmissionType.Manual, 40, StationsInitData.Frankfurt.Id);

    public static readonly Vehicle VwGolfSchwarz = Vehicle.From("VW Golf schwarz", VehicleCategory.Sedan,
        FuelType.Petrol, TransmissionType.Manual, 40, StationsInitData.Hamburg.Id);

    public static readonly Vehicle VwGolfSilber = Vehicle.From("VW Golf silber", VehicleCategory.Sedan, FuelType.Petrol,
        TransmissionType.Manual, 40, StationsInitData.Muenchen.Id);

    public static readonly Vehicle VwGolfGelb = Vehicle.From("VW Golf  gelb", VehicleCategory.Sedan, FuelType.Petrol,
        TransmissionType.Manual, 40, StationsInitData.Stuttgart.Id);

    public static readonly Vehicle AudiQ5Schwarz = Vehicle.From("Audi Q5 schwarz", VehicleCategory.Suv, FuelType.Petrol,
        TransmissionType.Automatic, 90, StationsInitData.Berlin.Id);

    public static readonly Vehicle AudiQ5Gruen = Vehicle.From("Audi Q5 gruen", VehicleCategory.Suv, FuelType.Petrol,
        TransmissionType.Automatic, 90, StationsInitData.Frankfurt.Id);

    public static readonly Vehicle AudiQ5Grau = Vehicle.From("Audi Q5 Grau", VehicleCategory.Suv, FuelType.Petrol,
        TransmissionType.Automatic, 90, StationsInitData.Hamburg.Id);

    public static readonly Vehicle AudiQ5Weiss = Vehicle.From("Audi Q5 Weiss", VehicleCategory.Suv, FuelType.Petrol,
        TransmissionType.Automatic, 90, StationsInitData.Muenchen.Id);

    public static readonly Vehicle AudiQ5Blau = Vehicle.From("Audi Q5 Blau", VehicleCategory.Suv, FuelType.Petrol,
        TransmissionType.Automatic, 90, StationsInitData.Stuttgart.Id);

    public static readonly Vehicle MercedesGlaSilber = Vehicle.From("Mercedes GLA silber", VehicleCategory.Suv,
        FuelType.Diesel, TransmissionType.Automatic, 70, StationsInitData.Berlin.Id);

    public static readonly Vehicle MercedesGlaRot = Vehicle.From("Mercedes GLA Rot", VehicleCategory.Suv,
        FuelType.Diesel, TransmissionType.Automatic, 70, StationsInitData.Frankfurt.Id);

    public static readonly Vehicle MercedesGlaBlau = Vehicle.From("Mercedes GLA Blau", VehicleCategory.Suv,
        FuelType.Diesel, TransmissionType.Automatic, 70, StationsInitData.Hamburg.Id);

    public static readonly Vehicle MercedesGlaWeiss = Vehicle.From("Mercedes GLA Weiss", VehicleCategory.Suv,
        FuelType.Diesel, TransmissionType.Automatic, 70, StationsInitData.Muenchen.Id);

    public static readonly Vehicle MercedesGlaHellblau = Vehicle.From("Mercedes GLA Hellblau", VehicleCategory.Suv,
        FuelType.Diesel, TransmissionType.Automatic, 70, StationsInitData.Stuttgart.Id);
}