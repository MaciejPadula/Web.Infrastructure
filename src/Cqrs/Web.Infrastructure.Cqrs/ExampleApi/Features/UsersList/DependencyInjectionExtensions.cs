using Web.Infrastructure.Cqrs.Mediator.Query;

namespace ExampleApi.Features.UsersList;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddUsersList(this IServiceCollection services)
    {
        services.AddScoped<IAsyncQueryHandler<GetUsersQuery>, GetUsersQueryHandler>();
        services.AddScoped<IUsersListService, UsersListService>();
        return services;
    }
}
