using Microsoft.Extensions.DependencyInjection;
using Web.Infrastructure.Microservices.Client.Builders;
using Web.Infrastructure.Microservices.Client.Interfaces;
using Web.Infrastructure.Microservices.Client.Logic;
using Web.Infrastructure.Microservices.Shared.Interfaces;

namespace Web.Infrastructure.Microservices.Client.Factories
{
    internal class MicroserviceCallerFactory
    {
        public static IMicroserviceCaller CreateHttp(IServiceProvider serviceProvider, MethodTypeBuilder methodBuilder, string interfaceNamespace, string serviceName)
        {
            return new HttpMicroserviceCaller(
                serviceProvider.GetRequiredService<IMethodEndpointProvider>(),
                new DefaultMethodTypeResolver(methodBuilder.Build()),
                serviceProvider.GetRequiredService<IHttpMessageProvider>(),
                serviceProvider.GetRequiredService<IResponseDeserializer>(),
                new HttpClient(),
                serviceProvider.GetRequiredService<IServiceLookup>().Lookup(serviceName) ??
                    serviceProvider.GetRequiredService<IServiceLookup>().Lookup(interfaceNamespace) ?? new Uri(string.Empty)
            );
        }
    }
}
