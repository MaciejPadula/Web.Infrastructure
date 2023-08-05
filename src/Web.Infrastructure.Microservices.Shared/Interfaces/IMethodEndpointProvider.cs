namespace Web.Infrastructure.Microservices.Shared.Interfaces
{
    public interface IMethodEndpointProvider
    {
        string ProvideEndpoint(string methodName, string serviceName);
        string GetControllerActionTemplate(string controllerName);
    }
}
