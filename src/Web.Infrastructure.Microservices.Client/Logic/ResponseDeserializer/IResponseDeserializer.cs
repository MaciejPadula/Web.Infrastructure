namespace Web.Infrastructure.Microservices.Client.Logic.ResponseDeserializer
{
    public interface IResponseDeserializer
    {
        object? Deserialize(string response, Type returnType);
        T? Deserialize<T>(string response);
    }
}
