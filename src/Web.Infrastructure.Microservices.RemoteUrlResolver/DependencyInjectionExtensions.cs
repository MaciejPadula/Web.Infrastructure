using Microsoft.Extensions.DependencyInjection;
using Web.Infrastructure.Microservices.Client.Interfaces;

namespace Web.Infrastructure.Microservices.RemoteUrlResolver;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddRemoteServiceLookup(this IServiceCollection services, Action<RemoteServiceLookupOptions> optionsPredicate)
    {
        var options = new RemoteServiceLookupOptions();
        optionsPredicate(options);

        services.AddScoped<IServiceLookup>(provider =>
            new RemoteServiceLookup(options));
        return services;
    }
}
