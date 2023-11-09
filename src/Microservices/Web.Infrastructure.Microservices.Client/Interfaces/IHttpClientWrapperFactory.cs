using Web.Infrastructure.Microservices.Client.Models;

namespace Web.Infrastructure.Microservices.Client.Interfaces;

internal interface IHttpClientWrapperFactory
{
    IHttpClientWrapper CreateHttpClient(MicroserviceClientConfiguration configuration);
}
