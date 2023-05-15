using Castle.DynamicProxy;
using Web.Infrastructure.Microservices.Client.Interfaces;

namespace Web.Infrastructure.Microservices.Client;

public class MicroserviceClient : AsyncInterceptorBase
{
    private readonly IMicroserviceCaller _microserviceCaller;
    private readonly IIncomingMethodValidator _incomingMethodValidator;

    internal MicroserviceClient(IMicroserviceCaller microserviceCaller, IIncomingMethodValidator incomingMethodValidator)
    {
        _microserviceCaller = microserviceCaller;
        _incomingMethodValidator = incomingMethodValidator;
    }

    protected override Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
    {
        return InterceptAsync<Task>(invocation, proceedInfo, default!);
    }

    protected override async Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
    {
        var targetMethod = invocation.Method;

        if (!_incomingMethodValidator.Validate(targetMethod, out var ex))
        {
            throw ex;
        }

        var result = await _microserviceCaller.Call<TResult>(
            targetMethod.Name, 
            targetMethod.DeclaringType?.Name ?? string.Empty, 
            invocation.Arguments);

        return result ?? default!;
    }
}
