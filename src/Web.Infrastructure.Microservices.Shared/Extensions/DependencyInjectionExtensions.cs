using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Web.Infrastructure.Microservices.Client.Logic;
using Web.Infrastructure.Microservices.Shared.Interfaces;

namespace Web.Infrastructure.Microservices.Shared.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMicroserviceEndpointResolver<T>(this IServiceCollection services)
            where T : class, IMethodEndpointProvider
        {
            services.TryAddTransient<IMethodEndpointProvider, T>();
            return services;
        }

        public static IServiceCollection AddMicroserviceEndpointResolver(this IServiceCollection services)
        {
            return services.AddMicroserviceEndpointResolver<DefaultMethodEndpointProvider>();
        }
    }
}