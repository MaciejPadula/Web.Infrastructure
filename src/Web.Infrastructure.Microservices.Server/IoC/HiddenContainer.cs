using Web.Infrastructure.Microservices.Client.Interfaces;

namespace Web.Infrastructure.Microservices.Server.IoC;

internal class HiddenContainer
{
    internal static IMethodEndpointProvider? MethodEndpointProvider { get; set; }
}
