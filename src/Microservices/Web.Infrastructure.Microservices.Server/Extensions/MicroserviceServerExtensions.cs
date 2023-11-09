using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Web.Infrastructure.Microservices.Shared.Interfaces;

namespace Web.Infrastructure.Microservices.Server.Extensions;

public static class MicroserviceServerExtensions
{
    public static IEndpointRouteBuilder RegisterMicroservice<TService, TController>(this IEndpointRouteBuilder builder)
        where TController : ControllerBase, TService
    {
        var controllerName = typeof(TController).Name.Replace("Controller", "");

        var provider = builder.CreateApplicationBuilder().ApplicationServices.GetRequiredService<IMethodEndpointProvider>();

        builder.MapControllerRoute(name: controllerName,
            pattern: provider.GetControllerActionTemplate(controllerName),
            defaults: new { controller = controllerName });

        return builder;
    }
}
