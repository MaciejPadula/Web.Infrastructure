using System.Threading.Tasks;

namespace Web.Infrastructure.Cqrs.Mediator.Command
{
    public interface IAsyncCommandHandler<T>
        where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
