namespace Web.Infrastructure.Microservices.Client.Logic.ServiceLookup
{
    public interface IServiceLookup
    {
        Uri Lookup(string serviceName);
    }
}
