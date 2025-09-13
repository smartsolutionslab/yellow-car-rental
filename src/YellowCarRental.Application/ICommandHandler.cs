namespace SmartSolutionsLab.YellowCarRental.Application;

public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}