using ExampleApi.UsersList;
using ExampleApi.UsersList.GetUsers;
using Microsoft.Extensions.DependencyInjection;
using Web.Infrastructure.Cqrs.Mediator.Query;

namespace ExampleApi.UsersList;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddUsersList(this IServiceCollection services)
    {
        services.AddScoped<IAsyncQueryHandler<GetUsersQuery>, GetUsersQueryHandler>();
        services.AddScoped<IUsersListService, UsersListService>();
        return services;
    }
}
