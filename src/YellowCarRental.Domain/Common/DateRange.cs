namespace SmartSolutionsLab.YellowCarRental.Domain;

public record DateRange(DateOnly Start, DateOnly End) : IValueObject
{
    public static DateRange From(DateOnly start, DateOnly end)
    {
        if (end < start) throw new ArgumentException("End before start");
        return new DateRange(start, end);
    }
    
    public static DateRange From(DateTime start, DateTime end) => From(DateOnly.FromDateTime(start), DateOnly.FromDateTime(end));

    public int TotalDaysInclusive()
    {
        // incl. last day (z.B. 01–05 => 5 days)
        return (End.ToDateTime(TimeOnly.MinValue) - Start.ToDateTime(TimeOnly.MinValue)).Days + 1;
    }
    
    public bool IsStarInPast => Start < DateOnly.FromDateTime(DateTime.UtcNow.Date);
    
    public bool IsStartInFuture => Start > DateOnly.FromDateTime(DateTime.UtcNow.Date);
    
   
    public bool Overlaps(DateRange other) =>
        !(End < other.Start || Start > other.End);
}