namespace SmartSolutionsLab.YellowCarRental.Domain;

public interface IStations : IRepository
{ 
    Task<IList<Station>> All();
    Task<Station> FindById(StationIdentifier id);
    Task Update(Station station);
}