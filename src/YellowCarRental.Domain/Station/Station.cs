namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed class Station : IRootEntity
{
    public StationIdentifier Id { get; }
    public StationName Name { get; }


    private Station(StationIdentifier id, StationName name)
    {
        Id = id;
        Name = name;
    }

    public static Station From(StationIdentifier id, StationName name) => new(id, name);
    

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