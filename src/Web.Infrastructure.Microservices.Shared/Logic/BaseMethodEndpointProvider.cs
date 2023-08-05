using Web.Infrastructure.Microservices.Shared.Interfaces;

namespace Web.Infrastructure.Microservices.Shared.Logic;

public abstract class BaseMethodEndpointProvider : IMethodEndpointProvider
{
    private readonly string _template;
    private readonly string[] _templateParts;

    public BaseMethodEndpointProvider(string template)
    {
        if (!template.Contains("{controller}"))
        {
            throw new ArgumentException("Template should contain {controller} string");
        }
        _template = template;
        _templateParts = _template.Split("/");
    }

    public string GetControllerActionTemplate(string controllerName)
    {
        return _template.Replace("controller", controllerName);
    }

    public string ProvideEndpoint(string methodName, string serviceName)
    {
        if (!_templateParts.Any()) return string.Empty;

        var controllerName = serviceName.Replace("I", "").Replace("Service", "");

        return $"{_templateParts[0].Replace("{controller}", controllerName)}/{_templateParts[1].Replace("{action}", methodName)}";
    }
}
