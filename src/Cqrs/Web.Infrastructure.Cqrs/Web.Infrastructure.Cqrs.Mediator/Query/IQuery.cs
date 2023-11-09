namespace Web.Infrastructure.Cqrs.Mediator.Query
{    
    public interface IQuery<T> : IQueryBase
    {
        T Result { get; set; }
    }
}