namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Station;

public record ListStationsQueryResult(IList<StationData> Stations)
{
    public static ListStationsQueryResult Empty => new ([]);
}