using Web.Infrastructure.Microservices.Client.Interfaces;

namespace Web.Infrastructure.Microservices.Shared.Logic;

public class BaseMethodEndpointProvider : IMethodEndpointProvider
{
    private readonly string _template;

    public BaseMethodEndpointProvider(string template)
    {
        if (!template.Contains("[controller]"))
        {
            throw new ArgumentException("Template should contain [controller] string");
        }
        _template = template;
    }

    public string GetControllerActionTemplate(string controllerName)
    {
        return _template.Replace("controller", controllerName);
    }

    public string Provide(string methodName, string serviceName)
    {
        return _template
            .Replace("[action]", methodName)
            .Replace("[controller]", serviceName);
    }
}
