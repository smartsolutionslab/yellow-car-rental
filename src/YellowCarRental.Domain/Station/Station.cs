namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed class Station : IRootEntity
{
    public StationIdentifier Id { get; }
    public StationName Name { get; }
    
    public StationAddress Address { get; private set; }


    private Station(StationIdentifier id, StationName name, StationAddress address)
    {
        Id = id;
        Name = name;
        Address = address;
    }

    public static Station From(StationIdentifier id, StationName name, StationAddress address) => new(id, name, address);
    

    private readonly List<VehicleIdentifier> _currentVehicleIds = [];

    public IReadOnlyCollection<VehicleIdentifier> CurrentVehicleIds => _currentVehicleIds.AsReadOnly();

    public bool HasVehicleAvailable(VehicleIdentifier vehicleId)
    {
        return _currentVehicleIds.Contains(vehicleId);
    }

    public void AssignVehicle(VehicleIdentifier vehicleId)
    {
        if (!_currentVehicleIds.Contains(vehicleId))
        {
            _currentVehicleIds.Add(vehicleId);
        }
    }

    public void RemoveVehicle(VehicleIdentifier vehicleId)
    {
        if (_currentVehicleIds.Contains(vehicleId))
        {
            _currentVehicleIds.Remove(vehicleId);
        }
    }
}