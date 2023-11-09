using System.Threading.Tasks;

namespace Web.Infrastructure.Cqrs.Mediator.Command
{
    public interface ICommandHandler<T>
        where T : ICommand
    {
        void Handle(T command);
    }
}