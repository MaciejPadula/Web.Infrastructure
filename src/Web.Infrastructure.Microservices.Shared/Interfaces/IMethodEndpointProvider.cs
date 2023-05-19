namespace Web.Infrastructure.Microservices.Client.Interfaces
{
    public interface IMethodEndpointProvider
    {
        string Provide(string methodName, string serviceName);
        string Template { get; }
    }
}
