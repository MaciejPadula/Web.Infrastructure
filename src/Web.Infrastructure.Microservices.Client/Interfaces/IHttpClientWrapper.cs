namespace Web.Infrastructure.Microservices.Client.Interfaces;

internal interface IHttpClientWrapper : IDisposable
{
    Task<HttpResponseMessage> SendAsync(Func<HttpRequestMessage> requestGenerator);
}
