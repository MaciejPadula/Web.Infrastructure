namespace Web.Infrastructure.Cqrs.Mediator.Query
{
    public interface IQueryHandler<TQuery>
        where TQuery : IQueryBase
    {
        void Handle(TQuery query);
    }
}