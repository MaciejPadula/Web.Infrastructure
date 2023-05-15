using Newtonsoft.Json;

namespace Web.Infrastructure.Microservices.Client.Logic.ResponseDeserializer
{
    internal class DefaultResponseDeserializer : IResponseDeserializer
    {
        public object? Deserialize(string response, Type returnType)
        {
            if (returnType == typeof(void))
            {
                return null;
            }

            return JsonConvert.DeserializeObject(response, returnType);
        }

        public T? Deserialize<T>(string response)
        {
            return (T?)Deserialize(response, typeof(T));
        }
    }
}
