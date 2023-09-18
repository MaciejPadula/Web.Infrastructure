using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Web.Infrastructure.Microservices.Client.Builders;
using Web.Infrastructure.Microservices.Client.Factories;
using Web.Infrastructure.Microservices.Client.Interfaces;
using Web.Infrastructure.Microservices.Client.Logic;
using Web.Infrastructure.Microservices.Shared.Extensions;

namespace Web.Infrastructure.Microservices.Client.Extensions
{
    public static class MicroserviceClientExtensions
    {
        private const ServiceLifetime DefaultServiceLifetime = ServiceLifetime.Scoped;

        public static IServiceCollection AddMicroserviceClient<TService>(this IServiceCollection services, ServiceLifetime? lifetime = null)
            where TService : class
        {
            return services.AddMicroserviceClient<TService>(x => { }, lifetime);
        }

        public static IServiceCollection AddMicroserviceClient<TService>(this IServiceCollection services, Action<MethodTypeBuilder> builder, ServiceLifetime? lifetime = null)
            where TService : class
        {
            var typeName = typeof(TService).FullName ?? throw new ArgumentNullException(nameof(TService));
            return services.AddMicroserviceClient<TService>(typeName, builder, lifetime);
        }

        public static IServiceCollection AddMicroserviceClient<TService>(this IServiceCollection services, string serviceName, ServiceLifetime? lifetime = null)
            where TService : class
        {
            return services.AddMicroserviceClient<TService>(serviceName, x => { }, lifetime);
        }

        public static IServiceCollection AddMicroserviceClient<TService>(this IServiceCollection services, string serviceName, Action<MethodTypeBuilder> builder, ServiceLifetime? lifetime = null)
            where TService : class
        {
            var methodBuilder = new MethodTypeBuilder();
            builder(methodBuilder);

            services.AddMicroserviceEndpointResolver();
            services.TryAddTransient<IHttpMessageProvider, DefaultHttpMessageProvider>();
            services.TryAddTransient<IServiceLookup, DefaultServiceLookup>();
            services.TryAddTransient<IResponseDeserializer, DefaultResponseDeserializer>();
            services.TryAddTransient<IIncomingMethodValidator, DefaultIncomingMethodValidator>();
            services.AddHttpClient();

            services.AddCastleCoreClient<TService>(provider => 
                MicroserviceCallerFactory.CreateHttp(
                    provider,
                    methodBuilder,
                    typeof(TService).Namespace ?? string.Empty,
                    serviceName),
                lifetime ?? DefaultServiceLifetime);

            return services;
        }

        private static IServiceCollection AddCastleCoreClient<TService>(this IServiceCollection services, Func<IServiceProvider, IMicroserviceCaller> options, ServiceLifetime lifetime)
            where TService : class
        {
            services.Add(new ServiceDescriptor(
                typeof(TService),
                s =>
                {
                    var generator = new ProxyGenerator();
                    var interceptor = new MicroserviceClient(options.Invoke(s), s.GetRequiredService<IIncomingMethodValidator>());

                    return generator.CreateInterfaceProxyWithoutTarget<TService>(interceptor.ToInterceptor());
                },
                lifetime)
            );

            return services;
        }
    }
}
