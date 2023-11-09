namespace ExampleApi.Repositories;

public interface IGenericRepository<T>
{
    Task Add(T entity);
    Task Update(T entity);
    Task<IEnumerable<T>> GetAll();
}
