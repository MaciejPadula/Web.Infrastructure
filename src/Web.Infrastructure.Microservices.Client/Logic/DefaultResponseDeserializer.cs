using Newtonsoft.Json;
using Web.Infrastructure.Microservices.Client.Interfaces;

namespace Web.Infrastructure.Microservices.Client.Logic
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
