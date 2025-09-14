using Microsoft.AspNetCore.Mvc;
using SmartSolutionsLab.YellowCarRental.Application;
using SmartSolutionsLab.YellowCarRental.Application.Contracts;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Customer;
using SmartSolutionsLab.YellowCarRental.Domain;

namespace SmartSolutionsLab.YellowCarRental.Api;

public static class CustomerEndpoints
{
    public static void MapApiCustomerEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet(
            "/api/customers/{id:guid}", 
            (Guid id, IQueryCommandHandler<ShowCustomerCommand, CustomerData> handler) =>
            handler.HandleQueryAsync(new ShowCustomerCommand(CustomerIdentifier.Of(id))));

        builder.MapPost(
            "/api/customers", 
            async (RegisterCustomerCommand command, ICommandHandler<RegisterCustomerCommand, CustomerIdentifier> handler, ICustomers customers) =>
        {
            var customerId = await handler.HandleAsync(command);
            var customer = (await customers.FindById(customerId)).ToData();
    
            return Results.Created($"/api/customers/{customer.Id}", customer);
        });
        
        builder.MapGet(
            "/api/customers/search", 
            async ([FromQuery] string searchTerm, [FromServices] IQueryCommandHandler<SearchCustomerCommand, SearchCustomersQueryResult> handler) =>
        {
            var result = await handler.HandleQueryAsync(new SearchCustomerCommand(new SearchTerm(searchTerm)));
            return Results.Ok(result);
        });
    }
}