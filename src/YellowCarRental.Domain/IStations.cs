namespace SmartSolutionsLab.YellowCarRental.Domain;

public interface IStations : IRepository
{ 
    Task<IList<Station>> All();
}