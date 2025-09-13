namespace SmartSolutionsLab.YellowCarRental.Domain;

public record DateRange(DateOnly Start, DateOnly End) : IValueObject
{
    public static DateRange Create(DateOnly start, DateOnly end)
    {
        if (end < start) throw new ArgumentException("End before start");
        if (start < DateOnly.FromDateTime(DateTime.UtcNow.Date)) throw new ArgumentException("Start in the past");
        return new DateRange(start, end);
    }

    public int TotalDaysInclusive()
    {
        // incl. last day (z.B. 01–05 => 5 days)
        return (End.ToDateTime(TimeOnly.MinValue) - Start.ToDateTime(TimeOnly.MinValue)).Days + 1;
    }

    public bool Overlaps(DateRange other) =>
        !(End < other.Start || Start > other.End);
}