using Web.Infrastructure.Cqrs.Mediator.Command;
using Web.Infrastructure.Cqrs.Mediator.Query;

namespace Web.Infrastructure.Cqrs.Mediator
{
    public interface IHandlersRepository
    {
        ICommandHandler<T> GetCommandHandler<T>() where T : ICommand;
        IAsyncCommandHandler<T> GetCommandAsyncHandler<T>() where T : ICommand;

        IQueryHandler<TQuery> GetQueryHandler<TQuery>() where TQuery : IQueryBase;
        IAsyncQueryHandler<TQuery> GetQueryAsyncHandler<TQuery>() where TQuery : IQueryBase;
    }
}
