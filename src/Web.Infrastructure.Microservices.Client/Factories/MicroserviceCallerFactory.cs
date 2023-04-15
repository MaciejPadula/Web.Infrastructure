using Microsoft.Extensions.DependencyInjection;
using Web.Infrastructure.Microservices.Client.Builders;
using Web.Infrastructure.Microservices.Client.Logic.HttpMessageProvider;
using Web.Infrastructure.Microservices.Client.Logic.MethodEndpointProvider;
using Web.Infrastructure.Microservices.Client.Logic.MethodTypeResolver;
using Web.Infrastructure.Microservices.Client.Logic.MicroserviceCaller;
using Web.Infrastructure.Microservices.Client.Logic.ResponseDeserializer;
using Web.Infrastructure.Microservices.Client.Logic.ServiceLookup;

namespace Web.Infrastructure.Microservices.Client.Factories
{
    internal class MicroserviceCallerFactory
    {
        public static IMicroserviceCaller CreateHttp(IServiceProvider serviceProvider, MethodTypeBuilder methodBuilder, string serviceName)
        {
            return new HttpMicroserviceCaller(
                serviceProvider.GetRequiredService<IMethodEndpointProvider>(),
                new DefaultMethodTypeResolver(methodBuilder.Build()),
                serviceProvider.GetRequiredService<IHttpMessageProvider>(),
                serviceProvider.GetRequiredService<IResponseDeserializer>(),
                serviceProvider.GetRequiredService<HttpClient>(),
                serviceProvider.GetRequiredService<IServiceLookup>().Lookup(serviceName)
            );
        }
    }
}
