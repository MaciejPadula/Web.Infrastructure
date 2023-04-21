using Newtonsoft.Json;
using System.Text;
using Web.Infrastructure.Microservices.Client.Logic.HttpMessageProvider;

namespace Web.Infrastructure.Microservices.Client.HttpMessageProvider
{
    internal class DefaultHttpMessageProvider : IHttpMessageProvider
    {
        public HttpRequestMessage Provide(Uri baseUrl, string endpoint, object?[]? args, HttpMethod method)
        {
            var uri = baseUrl.AbsoluteUri ?? "";

            var data = args?.Length > 0 ? JsonConvert.SerializeObject(args?[0] ?? new { }) : "{}";

            var content = new StringContent(data, Encoding.UTF8, "application/json");

            return new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri($"{uri}{endpoint}"),
                Content = content
            };
        }
    }
}
