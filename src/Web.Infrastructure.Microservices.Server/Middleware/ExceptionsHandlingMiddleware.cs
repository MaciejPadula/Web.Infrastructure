using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Web.Infrastructure.Microservices.Server.Middleware;

internal class ExceptionsHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(ex));
        }
    }
}
