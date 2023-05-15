using System.Reflection;
using Web.Infrastructure.Microservices.Client.Logic.IncomingMethodValidator;
using Web.Infrastructure.Microservices.Client.Logic.MicroserviceCaller;

namespace Web.Infrastructure.Microservices.Client
{
    internal class MicroserviceClient<T> : DispatchProxy
    {
        private IMicroserviceCaller? _microserviceCaller;
        private IIncomingMethodValidator? _incomingMethodValidator;

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            if (_microserviceCaller == null || _incomingMethodValidator == null || targetMethod == null || targetMethod.DeclaringType == null)
            {
                return null;
            }

            if (!_incomingMethodValidator.Validate(targetMethod, out var ex))
            {
                throw ex;
            }

            var genericArguments = targetMethod.ReturnType.GenericTypeArguments.ToList();

            var type = genericArguments.Count == 0 ? typeof(void) : genericArguments[0];

            var data = _microserviceCaller.Call(targetMethod.Name, targetMethod.DeclaringType.Name, args, type);
            return data;
        }

        public static T Create(IMicroserviceCaller microserviceCaller, IIncomingMethodValidator incomingMethodValidator)
        {
            object? proxy = Create<T, MicroserviceClient<T>>();

            var clientProxy = (MicroserviceClient<T>?)proxy;
            var outputProxy = (T?)proxy;

            if (clientProxy == null || outputProxy == null)
            {
                throw new Exception();
            }

            clientProxy.LoadProperties(microserviceCaller, incomingMethodValidator);
            return outputProxy;
        }

        private void LoadProperties(IMicroserviceCaller microserviceCaller, IIncomingMethodValidator incomingMethodValidator)
        {
            _microserviceCaller = microserviceCaller;
            _incomingMethodValidator = incomingMethodValidator;
        }
    }
}
