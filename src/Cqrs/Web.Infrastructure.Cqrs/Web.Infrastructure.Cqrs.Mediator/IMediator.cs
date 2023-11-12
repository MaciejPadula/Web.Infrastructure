using System.Threading.Tasks;
using Web.Infrastructure.Cqrs.Mediator.Command;
using Web.Infrastructure.Cqrs.Mediator.Query;

namespace Web.Infrastructure.Cqrs.Mediator
{
    public interface IMediator
    {
        void HandleCommand<TCommand>(TCommand command) where TCommand : ICommand;
        Task HandleCommandAsync<TCommand>(TCommand command) where TCommand : ICommand;

        TQuery HandleQuery<TQuery>(TQuery query) where TQuery : IQueryBase;
        Task<TQuery> HandleQueryAsync<TQuery>(TQuery query) where TQuery : IQueryBase;
    }
}