using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Web.Infrastructure.Microservices.Client.Builders;
using Web.Infrastructure.Microservices.Client.Factories;
using Web.Infrastructure.Microservices.Client.Interfaces;
using Web.Infrastructure.Microservices.Client.Logic;

namespace Web.Infrastructure.Microservices.Client.Extensions
{
    public static class MicroserviceClientExtensions
    {
        public static IServiceCollection AddMicroserviceClient<TService>(this IServiceCollection services)
            where TService : class
        {
            return services.AddMicroserviceClient<TService>(x => { });
        }

        public static IServiceCollection AddMicroserviceClient<TService>(this IServiceCollection services, Action<MethodTypeBuilder> builder)
            where TService : class
        {
            return services.AddMicroserviceClient<TService>(typeof(TService).FullName ?? throw new ArgumentNullException(), builder);
        }

        public static IServiceCollection AddMicroserviceClient<TService>(this IServiceCollection services, string serviceName)
            where TService : class
        {
            return services.AddMicroserviceClient<TService>(serviceName, x => { });
        }

        public static IServiceCollection AddMicroserviceClient<TService>(this IServiceCollection services, string serviceName, Action<MethodTypeBuilder> builder)
            where TService : class
        {
            var methodBuilder = new MethodTypeBuilder();
            builder.Invoke(methodBuilder);

            services.TryAddTransient<HttpClient>();
            services.TryAddTransient<IMethodEndpointProvider, DefaultMethodEndpointProvider>();
            services.TryAddTransient<IHttpMessageProvider, DefaultHttpMessageProvider>();
            services.TryAddScoped<IServiceLookup, DefaultServiceLookup>();
            services.TryAddTransient<IResponseDeserializer, DefaultResponseDeserializer>();
            services.TryAddTransient<IIncomingMethodValidator, DefaultIncomingMethodValidator>();

            services.AddCastleCoreClient<TService>(s => MicroserviceCallerFactory.CreateHttp(s, methodBuilder, serviceName));

            return services;
        }

        private static IServiceCollection AddCastleCoreClient<TService>(this IServiceCollection services, Func<IServiceProvider, IMicroserviceCaller> options)
            where TService : class
        {
            services.AddSingleton(s =>
            {
                var generator = new ProxyGenerator();
                var interceptor = new MicroserviceClient(options.Invoke(s), s.GetRequiredService<IIncomingMethodValidator>());

                return generator.CreateInterfaceProxyWithoutTarget<TService>(interceptor.ToInterceptor());
            });

            return services;
        }
    }
}
