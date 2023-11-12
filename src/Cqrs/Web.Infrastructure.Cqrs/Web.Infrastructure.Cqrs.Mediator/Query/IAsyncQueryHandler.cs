using System.Threading.Tasks;

namespace Web.Infrastructure.Cqrs.Mediator.Query
{
    public interface IAsyncQueryHandler<TQuery>
        where TQuery : IQueryBase
    {
        Task HandleAsync(TQuery query);
    }
}
