using Microsoft.Extensions.DependencyInjection;
using Web.Infrastructure.Cqrs.Mediator.Command;
using Web.Infrastructure.Cqrs.Mediator.Factory;

namespace Web.Infrastructure.Cqrs.Mediator.NetCoreExtensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        services.Add(new ServiceDescriptor(
            typeof(IMediator),
            p => p.CreateMediator(),
            serviceLifetime));

        return services;
    }

    private static IMediator CreateMediator(this IServiceProvider services) =>
        MediatorFactory.GetMediator(
            new MsDiHandlersRepository(services.GetRequiredService<IServiceScopeFactory>()));
}
