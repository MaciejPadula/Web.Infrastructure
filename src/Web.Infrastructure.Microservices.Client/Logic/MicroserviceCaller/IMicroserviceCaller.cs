using System.Reflection;

namespace Web.Infrastructure.Microservices.Client.Logic.MicroserviceCaller
{
    public interface IMicroserviceCaller
    {
        object? Call(MethodInfo method, object?[]? args);
    }
}
