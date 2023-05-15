using System.Reflection;

namespace Web.Infrastructure.Microservices.Client.Interfaces;

public interface IIncomingMethodValidator
{
    bool Validate(MethodInfo? methodInfo, out Exception ex);
}
