using System.Net;
using System.Reflection;
using Web.Infrastructure.Microservices.Client.Exceptions;
using Web.Infrastructure.Microservices.Client.Logic.HttpMessageProvider;
using Web.Infrastructure.Microservices.Client.Logic.MethodTypeResolver;
using Web.Infrastructure.Microservices.Client.Logic.ResponseDeserializer;

namespace Web.Infrastructure.Microservices.Client.Logic.MicroserviceCaller
{
    internal class HttpMicroserviceCaller : IMicroserviceCaller
    {
        private readonly IMethodTypeResolver _methodTypeResolver;
        private readonly IHttpMessageProvider _httpMessageProvider;
        private readonly IResponseDeserializer _responseDeserializer;
        private readonly HttpClient _httpClient;
        private readonly Uri _baseUrl;

        public HttpMicroserviceCaller(IMethodTypeResolver methodTypeResolver, IHttpMessageProvider httpMessageProvider, IResponseDeserializer responseDeserializer, HttpClient httpClient, Uri baseUrl)
        {
            _methodTypeResolver = methodTypeResolver;
            _httpMessageProvider = httpMessageProvider;
            _responseDeserializer = responseDeserializer;
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public object? Call(MethodInfo method)
        {
            var controller = method.DeclaringType?.Name.Replace("Service", "").Substring(1);
            var methodName = method.Name;

            var request = $"{controller}/{methodName}";

            var response = _httpClient?.Send(_httpMessageProvider.Provide(_baseUrl, request, _methodTypeResolver.Resolve(methodName)));

            if (!(response?.IsSuccessStatusCode ?? false))
            {
                throw new MicroserviceResponseException(response);
            }

            return _responseDeserializer.Deserialize(response?.Content?.ReadAsStringAsync().Result ?? "null", method);
        }
    }
}
