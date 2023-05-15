using System.Reflection;

namespace Web.Infrastructure.Microservices.Client.Logic.IncomingMethodValidator;

internal class DefaultIncomingMethodValidator : IIncomingMethodValidator
{
    public bool Validate(MethodInfo? methodInfo, out Exception ex)
    {
        if (methodInfo == null)
        {
            ex = new ArgumentNullException(nameof(methodInfo));
            return false;
        }

        var isGeneric = methodInfo.ReturnType.IsGenericType;
        var isGenericTask = isGeneric && methodInfo.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);
        var isNonGenericTask = !isGeneric && methodInfo.ReturnType == typeof(Task);

        if (!isGenericTask && !isNonGenericTask)
        {
            ex = new Exception(nameof(methodInfo.ReturnType));
            return false;
        }

        if (methodInfo.DeclaringType == null)
        {
            ex = new ArgumentNullException(nameof(methodInfo.DeclaringType));
            return false;
        }

        ex = default!;
        return true;
    }
}
