using System.Reflection;

namespace Web.Infrastructure.Microservices.Client.Logic.MethodEndpointProvider
{
    public interface IMethodEndpointProvider
    {
        string Provide(MethodInfo method);
    }
}
