namespace SmartSolutionsLab.YellowCarRental.Application;

public interface IQueryCommandHandler<in TCommand, TResult> where TCommand : IQueryCommand
{
    Task<TResult> HandleQueryAsync(TCommand command);   
}