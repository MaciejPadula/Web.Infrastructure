namespace Web.Infrastructure.Microservices.RemoteUrlResolver;

public class RemoteServiceLookupOptions
{
    public string ResolverEndpoint { get; set; }
    public HttpMethod Method { get; set; }

    internal RemoteServiceLookupOptions() { }
}
