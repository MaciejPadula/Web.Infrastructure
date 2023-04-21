using System.Reflection;

namespace Web.Infrastructure.Microservices.Client.Logic.ResponseDeserializer
{
    public interface IResponseDeserializer
    {
        object? Deserialize(string response, MethodInfo targetMethod);
    }
}
