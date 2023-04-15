namespace Web.Infrastructure.Microservices.Client.Builders
{
    public class MethodTypeBuilder
    {
        private readonly Dictionary<string, HttpMethod> _methods;

        internal MethodTypeBuilder()
        {
            _methods = new Dictionary<string, HttpMethod>();
        }

        public MethodTypeBuilder AddRequestMethodType(string methodName, HttpMethod requestType)
        {
            if (!_methods.ContainsKey(methodName))
            {
                _methods.Add(methodName, requestType);
            }

            return this;
        }

        internal Dictionary<string, HttpMethod> Build()
        {
            return _methods;
        }
    }
}
