using Microsoft.AspNetCore.Mvc;
using Web.Infrastructure.Microservices.Server.IoC;

namespace Web.Infrastructure.Microservices.Server;

public class MicroserviceControllerAttribute : RouteAttribute
{
    public MicroserviceControllerAttribute()
        : base(HiddenContainer.MethodEndpointProvider?.Template ?? throw new Exception("Endpoint provider not configured! Try RegisterMicroservice"))
    { }
}
