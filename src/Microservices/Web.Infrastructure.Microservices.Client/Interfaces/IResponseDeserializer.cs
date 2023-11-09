namespace Web.Infrastructure.Microservices.Client.Interfaces
{
    public interface IResponseDeserializer
    {
        object? Deserialize(string response, Type returnType);
        T? Deserialize<T>(string response);
    }
}
