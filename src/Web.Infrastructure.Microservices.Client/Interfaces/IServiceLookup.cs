namespace Web.Infrastructure.Microservices.Client.Interfaces
{
    public interface IServiceLookup
    {
        Uri Lookup(string serviceName);
    }
}
