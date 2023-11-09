using Web.Infrastructure.Microservices.Client.Interfaces;

namespace Web.Infrastructure.Microservices.Client.Logic
{
    internal class DefaultMethodTypeResolver : IMethodTypeResolver
    {
        private readonly Dictionary<string, HttpMethod> _methods;

        public DefaultMethodTypeResolver(Dictionary<string, HttpMethod> methods)
        {
            _methods = methods;
        }

        public HttpMethod Resolve(string methodName)
        {
            if (_methods?.TryGetValue(methodName, out var value) ?? false)
            {
                return value;
            }

            return HttpMethod.Post;
        }
    }
}
