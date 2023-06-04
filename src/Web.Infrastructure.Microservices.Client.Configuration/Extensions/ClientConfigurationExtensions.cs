using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Web.Infrastructure.Microservices.Client.Configuration.Logic;
using Web.Infrastructure.Microservices.Client.Interfaces;

namespace Web.Infrastructure.Microservices.Client.Configuration.Extensions
{
    public static class ClientConfigurationExtensions
    {
        private const string _microservicesConfigurationParent = "Microservices";

        public static IServiceCollection AddConfigurationServiceLookup(this IServiceCollection services, string? microservicesConfigurationParent)
        {
            services.TryAddScoped<IServiceLookup>(provider => 
                new ConfigurationServiceLookup(
                    provider.GetRequiredService<IConfiguration>(), 
                    microservicesConfigurationParent ?? _microservicesConfigurationParent)
            );
            return services;
        }
    }
}
