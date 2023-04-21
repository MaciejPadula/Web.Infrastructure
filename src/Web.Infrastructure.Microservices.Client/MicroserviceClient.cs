using System.Reflection;
using Web.Infrastructure.Microservices.Client.Logic.MicroserviceCaller;

namespace Web.Infrastructure.Microservices.Client
{
    internal class MicroserviceClient<T> : DispatchProxy
    {
        private IMicroserviceCaller? _microserviceCaller;

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            if (targetMethod == null)
            {
                throw new ArgumentNullException(nameof(targetMethod));
            }

            return _microserviceCaller?.Call(targetMethod, args);
        }

        public static T Create(IMicroserviceCaller microserviceCaller)
        {
            object? proxy = Create<T, MicroserviceClient<T>>();

            var clientProxy = (MicroserviceClient<T>?)proxy;
            var outputProxy = (T?)proxy;

            if (clientProxy == null || outputProxy == null)
            {
                throw new Exception();
            }

            clientProxy.LoadProperties(microserviceCaller);
            return outputProxy;
        }

        private void LoadProperties(IMicroserviceCaller microserviceCaller)
        {
            _microserviceCaller = microserviceCaller;
        }
    }
}
