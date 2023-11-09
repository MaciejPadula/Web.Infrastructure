using System.Reflection;

namespace Web.Infrastructure.Microservices.Client.Extensions;

internal static class MethodInfoExtensions
{
    internal static bool IsGeneric(this MethodInfo methodInfo) =>
        methodInfo.ReturnType.IsGenericType;

    internal static bool IsGenericTask(this MethodInfo methodInfo) =>
        methodInfo.IsGeneric() && methodInfo.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);

    internal static bool IsNonGenericTask(this MethodInfo methodInfo) =>
        !methodInfo.IsGeneric() && methodInfo.ReturnType == typeof(Task);
}
