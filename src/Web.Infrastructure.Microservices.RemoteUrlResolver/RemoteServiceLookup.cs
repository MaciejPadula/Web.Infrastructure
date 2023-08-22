using Web.Infrastructure.Microservices.Client.Interfaces;

namespace Web.Infrastructure.Microservices.RemoteUrlResolver;

public class RemoteServiceLookup : IServiceLookup
{
    private readonly RemoteServiceLookupOptions _options;

    internal RemoteServiceLookup(RemoteServiceLookupOptions options)
    {
        _options = options;
    }

    public Uri Lookup(string serviceName)
    {
        using var client = new HttpClient();

        var message = new HttpRequestMessage(_options.Method, serviceName);
        var response = client.Send(message);
        var urlFromResponse = response.Content.ReadAsStringAsync().Result;

        return new Uri(urlFromResponse);
    }
}
