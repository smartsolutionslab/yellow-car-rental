using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;

public class CustomerApiClient(HttpClient http, ILogger<CustomerApiClient> logger)
{
    public async Task<CustomerData> FindCustomerAsync(CustomerIdentifier id, CancellationToken cancellationToken = default)
    {
        logger.BeginScope("Finding customer with ID {CustomerId}", id);
        
        var customer = await http.GetFromJsonAsync<CustomerData>($"/api/customers/{id.Value}", cancellationToken);
        
        return customer ?? throw new ApiClientException($"Customer with ID {id} not found.");
    }
    
    public async Task<SearchCustomersQueryResult> SearchCustomersAsync(SearchTerm searchTerm, CancellationToken cancellationToken = default)
    {
        logger.BeginScope("Searching customers with name {searchTerm}", searchTerm);
        
        var url = $"/api/customers/search?searchTerm={Uri.EscapeDataString(searchTerm.Value)}";
        var result = await http.GetFromJsonAsync<SearchCustomersQueryResult>(url, cancellationToken);
        return result ?? SearchCustomersQueryResult.Empty;
    }

    public async Task<CustomerData> RegisterCustomerAsync(RegisterCustomerCommand command, CancellationToken cancellationToken = default)
    {
        logger.BeginScope("Registering new customer with name {CustomerName}", command.Name);
        
        var response = await http.PostAsJsonAsync("/api/customers", command, cancellationToken);
        response.EnsureSuccessStatusCode();
        var createdCustomer = await response.Content.ReadFromJsonAsync<CustomerData>(cancellationToken);
        return createdCustomer ?? throw new ApiClientException("Failed to deserialize created customer.");
    }
}