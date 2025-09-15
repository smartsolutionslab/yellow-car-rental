namespace SmartSolutionsLab.YellowCarRental.Domain;

public sealed class Station : IRootEntity
{
    public sealed record VehicleAssignment(StationIdentifier StationId, VehicleIdentifier VehicleId)
    {
        private Guid _id = Guid.Empty;

        private VehicleAssignment() : this(null!, null!) // For EF
        { }

        public static VehicleAssignment From(StationIdentifier stationId, VehicleIdentifier vehicleId)
        {
            return new VehicleAssignment(stationId, vehicleId);
        }
    }
    
    public StationIdentifier Id { get; } = null!;
    public StationName Name { get; } = null!;
    
    public StationAddress Address { get; private set; } = null!;


    private Station() // for EF
    {}
    
    private Station(StationIdentifier id, StationName name, StationAddress address)
    {
        Id = id;
        Name = name;
        Address = address;
    }

    public static Station From(StationIdentifier id, StationName name, StationAddress address) => new(id, name, address);
    

    private List<VehicleAssignment> _vehicleAssignments = [];

    public IReadOnlyCollection<VehicleIdentifier> CurrentVehicleIds
    {
        get => _vehicleAssignments.Select(a => a.VehicleId).ToList();
        //protected init => _currentVehicleIds = value.ToList();
    }

    public bool HasVehicleAvailable(VehicleIdentifier vehicleId)
    {
        return _vehicleAssignments.Any(a => a.VehicleId.Value == vehicleId.Value);
    }

    public void AssignVehicle(VehicleIdentifier vehicleId)
    {
        if (_vehicleAssignments.All(a => a.VehicleId.Value != vehicleId.Value))
        {
            _vehicleAssignments.Add(VehicleAssignment.From(Id, vehicleId));
        }
    }

    public void RemoveVehicle(VehicleIdentifier vehicleId)
    {
        var foundAssignment = _vehicleAssignments.Find(a => a.VehicleId.Value == vehicleId.Value);
        
        if(foundAssignment != null) _vehicleAssignments.Remove(foundAssignment);
    }
}