using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Web.Infrastructure.Microservices.Client.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Web.Infrastructure.Microservices.Client.Logic;

namespace Web.Infrastructure.Microservices.Server.Extensions
{
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

        public static IServiceCollection AddMicroservicesEndpointResolver<T>(this IServiceCollection services)
            where T : class, IMethodEndpointProvider
        {
            services.TryAddSingleton<IMethodEndpointProvider, T>();
            return services;
        }

        public static IServiceCollection AddMicroservicesEndpointResolver(this IServiceCollection services)
        {
            return services.AddMicroservicesEndpointResolver<DefaultMethodEndpointProvider>();
        }

        //public static IServiceCollection TryRegisterEndpointProvider(this IServiceCollection services, IMethodEndpointProvider provider)
        //{
        //    services.TryAddTransient(s =>
        //    {
        //        HiddenContainer.MethodEndpointProvider = provider;
        //        return provider;
        //    });

        //    return services;
        //}
    }
}
