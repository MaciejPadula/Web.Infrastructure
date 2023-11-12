using Microsoft.Extensions.DependencyInjection;
using Web.Infrastructure.Cqrs.Mediator.Command;
using Web.Infrastructure.Cqrs.Mediator.Query;

namespace Web.Infrastructure.Cqrs.Mediator.NetCoreExtensions;

internal class MsDiHandlersRepository : IHandlersRepository
{
    private readonly IServiceScope _scope;

    public MsDiHandlersRepository(IServiceScopeFactory scopeFactory)
    {
        _scope = scopeFactory.CreateScope();
    }

    public IAsyncCommandHandler<T> GetCommandAsyncHandler<T>() where T : ICommand
    {
        return GetRequiredService<IAsyncCommandHandler<T>>();
    }

    public ICommandHandler<T> GetCommandHandler<T>() where T : ICommand
    {
        return GetRequiredService<ICommandHandler<T>>();
    }

    public IAsyncQueryHandler<TQuery> GetQueryAsyncHandler<TQuery>() where TQuery : IQueryBase
    {
        return GetRequiredService<IAsyncQueryHandler<TQuery>>();
    }

    public IQueryHandler<TQuery> GetQueryHandler<TQuery>() where TQuery : IQueryBase
    {
        return GetRequiredService<IQueryHandler<TQuery>>();
    }

    private T GetRequiredService<T>() where T : class =>
        _scope.ServiceProvider.GetRequiredService<T>();
}
