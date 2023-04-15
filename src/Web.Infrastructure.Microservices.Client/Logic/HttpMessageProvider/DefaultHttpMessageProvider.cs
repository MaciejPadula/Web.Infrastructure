using Web.Infrastructure.Microservices.Client.Logic.HttpMessageProvider;

namespace Web.Infrastructure.Microservices.Client.HttpMessageProvider
{
    internal class DefaultHttpMessageProvider : IHttpMessageProvider
    {
        public HttpRequestMessage Provide(Uri baseUrl, string endpoint, HttpMethod method)
        {
            var uri = baseUrl.AbsoluteUri ?? "";
            return new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri($"{uri}{endpoint}")
            };
        }
    }
}
