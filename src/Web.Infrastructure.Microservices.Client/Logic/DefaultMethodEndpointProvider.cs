using Web.Infrastructure.Microservices.Client.Interfaces;

namespace Web.Infrastructure.Microservices.Client.Logic
{
    internal class DefaultMethodEndpointProvider : IMethodEndpointProvider
    {
        public string Provide(string methodName, string serviceName)
        {
            var controller = serviceName.Replace("Service", "").Substring(1);

            return $"{controller}/{methodName}";
        }
    }
}
