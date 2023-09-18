using System.Reflection;
using Web.Infrastructure.Microservices.Client.Extensions;
using Web.Infrastructure.Microservices.Client.Interfaces;

namespace Web.Infrastructure.Microservices.Client.Logic;

internal class DefaultIncomingMethodValidator : IIncomingMethodValidator
{
    public bool Validate(MethodInfo? methodInfo, out Exception ex)
    {
        if (methodInfo is null)
        {
            ex = new ArgumentNullException(nameof(methodInfo));
            return false;
        }

        if (!methodInfo.IsGenericTask() && !methodInfo.IsNonGenericTask())
        {
            ex = new Exception(nameof(methodInfo.ReturnType));
            return false;
        }

        if (methodInfo.DeclaringType is null)
        {
            ex = new NullReferenceException(nameof(methodInfo.DeclaringType));
            return false;
        }

        ex = default!;
        return true;
    }
}
