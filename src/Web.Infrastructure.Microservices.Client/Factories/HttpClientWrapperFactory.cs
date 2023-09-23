using Microsoft.Extensions.Logging;
using Web.Infrastructure.Microservices.Client.Interfaces;
using Web.Infrastructure.Microservices.Client.Logic;
using Web.Infrastructure.Microservices.Client.Models;

namespace Web.Infrastructure.Microservices.Client.Factories;

internal class HttpClientWrapperFactory : IHttpClientWrapperFactory
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILoggerFactory _loggerFactory;

    public HttpClientWrapperFactory(IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
    {
        _httpClientFactory = httpClientFactory;
        _loggerFactory = loggerFactory;
    }

    public IHttpClientWrapper CreateHttpClient(MicroserviceClientConfiguration configuration) =>
        new HttpClientWrapper(
            _httpClientFactory.CreateClient(),
            configuration,
            _loggerFactory.CreateLogger<HttpClientWrapper>());
}