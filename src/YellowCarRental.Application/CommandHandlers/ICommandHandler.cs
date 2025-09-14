using SmartSolutionsLab.YellowCarRental.Application.Contracts;

namespace SmartSolutionsLab.YellowCarRental.Application;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}

public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand
{
    Task<TResult> HandleAsync(TCommand command);
}