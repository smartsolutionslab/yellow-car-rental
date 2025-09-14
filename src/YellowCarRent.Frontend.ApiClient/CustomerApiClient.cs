using System.Net.Http.Json;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;

namespace SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;

public class CustomerApiClient(HttpClient http)
{
    public async Task<CustomerData> FindCustomerAsync(Guid id)
    {
        var customer = await http.GetFromJsonAsync<CustomerData>($"/api/customers/{id}");
        
        return customer ?? throw new InvalidOperationException($"Customer with ID {id} not found.");
    }

    public async Task<CustomerData> RegisterCustomerAsync(RegisterCustomerCommand command)
    {
        var response = await http.PostAsJsonAsync("/api/customers", command);
        response.EnsureSuccessStatusCode();
        var createdCustomer = await response.Content.ReadFromJsonAsync<CustomerData>();
        return createdCustomer ?? throw new InvalidOperationException("Failed to deserialize created customer.");


    }
}