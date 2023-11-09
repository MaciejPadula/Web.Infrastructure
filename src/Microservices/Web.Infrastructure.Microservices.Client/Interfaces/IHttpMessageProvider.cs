namespace Web.Infrastructure.Microservices.Client.Interfaces
{
    public interface IHttpMessageProvider
    {
        HttpRequestMessage Provide(Uri baseUrl, string endpoint, object?[]? args, HttpMethod method);
    }
}
