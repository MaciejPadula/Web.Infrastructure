using System.Threading.Tasks;
using Web.Infrastructure.Cqrs.Mediator.Command;
using Web.Infrastructure.Cqrs.Mediator.Query;

namespace Web.Infrastructure.Cqrs.Mediator
{
    internal class Mediator : IMediator
    {
        private readonly IHandlersRepository _handlersRepository;

        internal Mediator(IHandlersRepository handlersRepository)
        {
            _handlersRepository = handlersRepository;
        }

        public void HandleCommand<T>(T command) where T : ICommand
        {
            var handler = _handlersRepository.GetCommandHandler<T>();
            handler.Handle(command);
        }

        public async Task HandleCommandAsync<T>(T command) where T : ICommand
        {
            var handler = _handlersRepository.GetCommandAsyncHandler<T>();
            await handler.HandleAsync(command);
        }

        public void HandleQuery<TQuery>(TQuery query) where TQuery : IQueryBase
        {
            var handler = _handlersRepository.GetQueryHandler<TQuery>();
            handler.Handle(query);
        }

        public async Task HandleQueryAsync<TQuery>(TQuery query) where TQuery : IQueryBase
        {
            var handler = _handlersRepository.GetQueryAsyncHandler<TQuery>();
            await handler.HandleAsync(query);
        }
    }
}