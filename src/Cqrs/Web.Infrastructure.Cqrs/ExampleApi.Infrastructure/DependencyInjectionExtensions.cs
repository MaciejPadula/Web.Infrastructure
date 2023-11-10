using ExampleApi.Infrastructure.Repositories;
using ExampleApi.Model;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleApi.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IUsersRepository, UsersRepository>();
        return services;
    }
}
