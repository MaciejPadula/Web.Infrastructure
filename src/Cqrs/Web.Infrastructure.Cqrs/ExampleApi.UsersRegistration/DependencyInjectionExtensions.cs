using ExampleApi.UsersRegistration;
using ExampleApi.UsersRegistration.AddUser;
using ExampleApi.UsersRegistration.FindUser;
using ExampleApi.UsersRegistration.ValidateEmail;
using Microsoft.Extensions.DependencyInjection;
using Web.Infrastructure.Cqrs.Mediator.Command;
using Web.Infrastructure.Cqrs.Mediator.Query;

namespace ExampleApi.UsersRegistration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddUsersRegistration(this IServiceCollection services)
    {
        services.AddScoped<IQueryHandler<ValidateEmailQuery>, ValidateEmailQueryHandler>();
        services.AddScoped<IAsyncQueryHandler<FindUserQuery>, FindUserQueryHandler>();
        services.AddScoped<IAsyncCommandHandler<AddUserCommand>, AddUserCommandHandler>();
        services.AddScoped<IRegisterUserService, RegisterUserService>();
        return services;
    }
}
