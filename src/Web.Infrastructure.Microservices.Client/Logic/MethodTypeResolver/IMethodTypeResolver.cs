namespace Web.Infrastructure.Microservices.Client.Logic.MethodTypeResolver
{
    public interface IMethodTypeResolver
    {
        HttpMethod Resolve(string methodName);
    }
}
