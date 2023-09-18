using Web.Infrastructure.Microservices.Client.Interfaces;

namespace Web.Infrastructure.Microservices.Client.Logic
{
    internal class DefaultServiceLookup : IServiceLookup
    {
        public Uri? Lookup(string serviceName)
        {
            return new Uri($"{serviceName}");
        }
    }
}
