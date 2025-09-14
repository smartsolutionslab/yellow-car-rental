using SmartSolutionsLab.YellowCarRent.Frontend.ApiClient;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Frontend.Public.Client;

public interface ICurrentUserProfile
{
    Task InitializeAsync();
    CustomerData? GetCustomerData();
}

public class CurrentUserProfile(CustomerApiClient customerApi) : ICurrentUserProfile
{
    private CustomerData _customerData;
    private bool _isInitialized = false;
    
    public async Task InitializeAsync()
    {
        if(_isInitialized) return;
        
        // for demo purposes only
        var demoCustomerId = CustomerIdentifier.Of("8f5d9c4a-2b3e-4f1a-9d6e-7c8b2a1f3e45");
        _customerData = await customerApi.FindCustomerAsync(demoCustomerId);
        
        _isInitialized = true;
    }
    public CustomerData? GetCustomerData() => _customerData;
}