namespace Web.Infrastructure.Microservices.Client.Interfaces
{
    public interface IMethodTypeResolver
    {
        HttpMethod Resolve(string methodName);
    }
}
