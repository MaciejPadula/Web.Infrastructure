namespace Web.Infrastructure.Microservices.Client.Logic.MethodEndpointProvider
{
    public interface IMethodEndpointProvider
    {
        string Provide(string methodName, string serviceName);
    }
}
