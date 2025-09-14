namespace SmartSolutionsLab.YellowCarRental.Infrastructure.Persistence;

public class PersistenceException : Exception
{
    public PersistenceException(string message) : base(message)
    {}
    
    public PersistenceException(string message, Exception innerException) : base(message, innerException)
    {}
}