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
            var microserviceUrl = GetMicroserviceUrl(
                serviceProvider.GetRequiredService<IServiceLookup>(),
                serviceName,
                interfaceNamespace);

            return new HttpMicroserviceCaller(
                serviceProvider.GetRequiredService<IMethodEndpointProvider>(),
                new DefaultMethodTypeResolver(methodBuilder.Build()),
                serviceProvider.GetRequiredService<IHttpMessageProvider>(),
                serviceProvider.GetRequiredService<IResponseDeserializer>(),
                serviceProvider.GetRequiredService<IHttpClientFactory>(),
                microserviceUrl);
        }

        private static Uri GetMicroserviceUrl(IServiceLookup serviceLookup, string serviceName, string namespaceName) =>
            serviceLookup.Lookup(serviceName) ??
            serviceLookup.Lookup(namespaceName) ??
            new Uri(string.Empty);

    }
}
