using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Web.Infrastructure.Microservices.Client.Builders;
using Web.Infrastructure.Microservices.Client.Factories;
using Web.Infrastructure.Microservices.Client.HttpMessageProvider;
using Web.Infrastructure.Microservices.Client.Logic.HttpMessageProvider;
using Web.Infrastructure.Microservices.Client.Logic.MethodEndpointProvider;
using Web.Infrastructure.Microservices.Client.Logic.MethodTypeResolver;
using Web.Infrastructure.Microservices.Client.Logic.MicroserviceCaller;
using Web.Infrastructure.Microservices.Client.Logic.ResponseDeserializer;
using Web.Infrastructure.Microservices.Client.Logic.ServiceLookup;

namespace Web.Infrastructure.Microservices.Client.Extensions
{
    public static class MicroserviceClientExtensions
    {
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
            services.TryAddTransient<IHttpMessageProvider, DefaultHttpMessageProvider>();
            services.TryAddTransient<IServiceLookup, DefaultServiceLookup>();
            services.TryAddTransient<IResponseDeserializer, DefaultResponseDeserializer>();

            services.AddScoped(s =>
            {
                var caller = MicroserviceCallerFactory.CreateHttp(s, methodBuilder, serviceName);

                return MicroserviceClient<TService>.Create(caller);
            });



            return services;
        }
    }
}
