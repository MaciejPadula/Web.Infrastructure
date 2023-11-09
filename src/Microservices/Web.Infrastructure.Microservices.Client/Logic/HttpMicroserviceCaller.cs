using System.Net;
using System.Net.Http;
using Web.Infrastructure.Microservices.Client.Exceptions;
using Web.Infrastructure.Microservices.Client.Interfaces;
using Web.Infrastructure.Microservices.Client.Models;
using Web.Infrastructure.Microservices.Shared.Interfaces;

namespace Web.Infrastructure.Microservices.Client.Logic
{
    internal class HttpMicroserviceCaller : IMicroserviceCaller
    {
        private readonly IMethodEndpointProvider _methodEndpointProvider;
        private readonly IMethodTypeResolver _methodTypeResolver;
        private readonly IHttpMessageProvider _httpMessageProvider;
        private readonly IResponseDeserializer _responseDeserializer;
        private readonly IHttpClientWrapperFactory _httpClientFactory;
        private readonly MicroserviceClientConfiguration _configuration;

        public HttpMicroserviceCaller(
            IMethodEndpointProvider methodEndpointProvider,
            IMethodTypeResolver methodTypeResolver,
            IHttpMessageProvider httpMessageProvider,
            IResponseDeserializer responseDeserializer,
            IHttpClientWrapperFactory httpClientFactory,
            MicroserviceClientConfiguration configuration)
        {
            _methodEndpointProvider = methodEndpointProvider;
            _methodTypeResolver = methodTypeResolver;
            _httpMessageProvider = httpMessageProvider;
            _responseDeserializer = responseDeserializer;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<TResult?> Call<TResult>(string methodName, string serviceName, object?[]? args)
        {
            return (TResult?)await Call(methodName, serviceName, args, typeof(TResult));
        }

        public async Task<object?> Call(string methodName, string serviceName, object?[]? args, Type type)
        {
            var endpoint = _methodEndpointProvider.ProvideEndpoint(methodName, serviceName);

            var requestGenerator = () => _httpMessageProvider.Provide(
                _configuration.BaseUrl,
                endpoint,
                args,
                _methodTypeResolver.Resolve(methodName)
            );

            try
            {
                using var client = _httpClientFactory.CreateHttpClient(_configuration);
                var response = await client.SendAsync(requestGenerator);

                if (response is null)
                {
                    throw new NullReferenceException(nameof(response));
                }

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new MicroserviceResponseException(await response.Content.ReadAsStringAsync(), response.StatusCode);
                }

                return _responseDeserializer.Deserialize(response?.Content?.ReadAsStringAsync().Result ?? "null", type);
            }
            catch (TaskCanceledException ex)
            {
                throw new MicroserviceResponseException(ex.Message, HttpStatusCode.RequestTimeout, ex);
            }
            catch (HttpRequestException ex)
            {
                throw new MicroserviceResponseException(ex.Message, ex.StatusCode ?? HttpStatusCode.NotFound, ex);
            }
        }
    }
}
