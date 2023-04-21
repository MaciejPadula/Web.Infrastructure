using Newtonsoft.Json;
using System.Reflection;

namespace Web.Infrastructure.Microservices.Client.Logic.ResponseDeserializer
{
    internal class DefaultResponseDeserializer : IResponseDeserializer
    {
        public object? Deserialize(string response, MethodInfo targetMethod)
        {
            if (targetMethod.ReturnType == typeof(void))
            {
                return null;
            }

            return JsonConvert.DeserializeObject(response, targetMethod.ReturnType);
        }
    }
}
