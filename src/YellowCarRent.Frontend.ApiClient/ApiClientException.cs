namespace SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;

public class ApiClientException : Exception
{
    public ApiClientException(string message) : base(message)
    {
    }
    
    public ApiClientException(string message, Exception innerException) : base(message, innerException)
    {
    }
}