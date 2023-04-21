namespace Web.Infrastructure.Microservices.Client.Logic.ServiceLookup
{
    internal class DefaultServiceLookup : IServiceLookup
    {
        public Uri Lookup(string serviceName)
        {
            return new Uri($"http://{serviceName}");
        }
    }
}
