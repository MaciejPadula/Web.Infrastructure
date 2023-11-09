using Web.Infrastructure.Cqrs.Mediator.Command;
using Web.Infrastructure.Cqrs.Mediator.Query;

namespace ExampleApi.Features.UsersRegistration;

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
