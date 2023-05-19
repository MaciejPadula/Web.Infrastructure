using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Web.Infrastructure.Microservices.Client.Interfaces;
using Web.Infrastructure.Microservices.Client.Logic;
using Web.Infrastructure.Microservices.Server.IoC;

namespace Web.Infrastructure.Microservices.Server.Extensions
{
    public static class MicroserviceServerExtensions
    {
        public static IServiceCollection RegisterMicroservice<TService>(this IServiceCollection services)
            where TService : class
        {
            services.TryRegisterEndpointProvider(new DefaultMethodEndpointProvider());
            return services;
        }

        public static IServiceCollection TryRegisterEndpointProvider(this IServiceCollection services, IMethodEndpointProvider provider)
        {
            services.TryAddTransient(s =>
            {
                HiddenContainer.MethodEndpointProvider = provider;
                return provider;
            });

            return services;
        }
    }
}
