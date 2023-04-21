using System.Reflection;

namespace Web.Infrastructure.Microservices.Client.Logic.MethodEndpointProvider
{
    internal class DefaultMethodEndpointProvider : IMethodEndpointProvider
    {
        public string Provide(MethodInfo method)
        {
            var controller = method.DeclaringType?.Name.Replace("Service", "").Substring(1);
            var methodName = method.Name;

            return $"{controller}/{methodName}";
        }
    }
}
