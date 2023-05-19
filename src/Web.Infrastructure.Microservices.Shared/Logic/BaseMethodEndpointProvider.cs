using Web.Infrastructure.Microservices.Client.Interfaces;

namespace Web.Infrastructure.Microservices.Shared.Logic;

public class BaseMethodEndpointProvider : IMethodEndpointProvider
{
    private readonly string _template;

    public BaseMethodEndpointProvider(string template)
    {
        if (!template.Contains("[action]"))
        {
            throw new ArgumentException("Template should contain [action] string");
        }
        _template = template;
    }

    public string Template => _template;

    public string Provide(string methodName, string serviceName)
    {
        return _template
            .Replace("[action]", methodName)
            .Replace("[controller]", serviceName);
    }
}
