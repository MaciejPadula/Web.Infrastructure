using System.Text;
using System.Text.Json;
using Web.Infrastructure.Microservices.Client.Interfaces;

namespace Web.Infrastructure.Microservices.Client.Logic
{
    internal class DefaultHttpMessageProvider : IHttpMessageProvider
    {
        public HttpRequestMessage Provide(Uri baseUrl, string endpoint, object?[]? args, HttpMethod method)
        {
            var uri = baseUrl.AbsoluteUri ?? "";

            var data = args?.Length > 0 ? JsonSerializer.Serialize(args?[0] ?? new { }) : "{}";

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
