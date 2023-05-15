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

        public async Task<TResult?> Call<TResult>(string methodName, string controllerName, object?[]? args)
        {
            return (TResult?)await Call(methodName, controllerName, args, typeof(TResult));
        }

        public async Task<object?> Call(string methodName, string controllerName, object?[]? args, Type type)
        {
            var endpoint = _methodEndpointProvider.Provide(methodName, controllerName);

            var message = _httpMessageProvider.Provide(
                _baseUrl,
                endpoint,
                args,
                _methodTypeResolver.Resolve(methodName)
            );

            var response = await _httpClient.SendAsync(message);

            if (!(response?.IsSuccessStatusCode ?? false))
            {
                throw new MicroserviceResponseException(response);
            }

            return _responseDeserializer.Deserialize(response?.Content?.ReadAsStringAsync().Result ?? "null", type);
        }
    }
}
