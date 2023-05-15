using Microsoft.Extensions.DependencyInjection;
using Web.Infrastructure.Microservices.Client.Builders;
using Web.Infrastructure.Microservices.Client.Interfaces;
using Web.Infrastructure.Microservices.Client.Logic;

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
