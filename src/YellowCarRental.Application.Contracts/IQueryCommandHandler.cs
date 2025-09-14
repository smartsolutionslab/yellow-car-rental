namespace SmartSolutionsLab.YellowCarRental.Application.Contracts;

public interface IQueryCommandHandler<in TCommand, TResult> where TCommand : IQueryCommand
{
    Task<TResult> HandleQueryAsync(TCommand command);   
}