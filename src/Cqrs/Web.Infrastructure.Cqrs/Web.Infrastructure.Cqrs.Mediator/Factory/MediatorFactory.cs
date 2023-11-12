using Web.Infrastructure.Cqrs.Mediator.Command;
using Web.Infrastructure.Cqrs.Mediator.Query;

namespace Web.Infrastructure.Cqrs.Mediator.Factory
{
    public static class MediatorFactory
    {
        public static IMediator GetMediator(
            IHandlersRepository handlersRepository)
        {
            return new Mediator(handlersRepository);
        }
    }
}