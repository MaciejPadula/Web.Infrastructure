using System.Threading.Tasks;
using Web.Infrastructure.Cqrs.Mediator.Command;
using Web.Infrastructure.Cqrs.Mediator.Query;

namespace Web.Infrastructure.Cqrs.Mediator
{
    public interface IMediator
    {
        void HandleCommand<T>(T command)
             where T : ICommand;
        Task HandleCommandAsync<T>(T command)
             where T : ICommand;

        void HandleQuery<TQuery>(TQuery query) where TQuery : IQueryBase;
        Task HandleQueryAsync<TQuery>(TQuery query) where TQuery : IQueryBase;
    }
}