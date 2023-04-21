using System.Net;
using System.Reflection;
using Web.Infrastructure.Microservices.Client.Exceptions;
using Web.Infrastructure.Microservices.Client.Logic.HttpMessageProvider;
using Web.Infrastructure.Microservices.Client.Logic.MethodEndpointProvider;
using Web.Infrastructure.Microservices.Client.Logic.MethodTypeResolver;
using Web.Infrastructure.Microservices.Client.Logic.ResponseDeserializer;

namespace Web.Infrastructure.Microservices.Client.Logic.MicroserviceCaller
{
    internal class HttpMicroserviceCaller : IMicroserviceCaller
    {
        private readonly IMethodEndpointProvider _methodEndpointProvider;
        private readonly IMethodTypeResolver _methodTypeResolver;
        private readonly IHttpMessageProvider _httpMessageProvider;
        private readonly IResponseDeserializer _responseDeserializer;
        private readonly HttpClient _httpClient;
        private readonly Uri _baseUrl;

        public HttpMicroserviceCaller(IMethodEndpointProvider methodEndpointProvider, IMethodTypeResolver methodTypeResolver, IHttpMessageProvider httpMessageProvider, IResponseDeserializer responseDeserializer, HttpClient httpClient, Uri baseUrl)
        {
            _methodEndpointProvider = methodEndpointProvider;
            _methodTypeResolver = methodTypeResolver;
            _httpMessageProvider = httpMessageProvider;
            _responseDeserializer = responseDeserializer;
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public object? Call(MethodInfo method, object?[]? args)
        {
            var endpoint = _methodEndpointProvider.Provide(method);

            var message = _httpMessageProvider.Provide(
                _baseUrl,
                endpoint,
                args,
                _methodTypeResolver.Resolve(method.Name)
            );

            var response = _httpClient?.Send(message);

            if (!(response?.IsSuccessStatusCode ?? false))
            {
                throw new MicroserviceResponseException(response);
            }

            return _responseDeserializer.Deserialize(response?.Content?.ReadAsStringAsync().Result ?? "null", method);
        }
    }
}
