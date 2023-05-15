using System.Reflection;

namespace Web.Infrastructure.Microservices.Client.Logic.IncomingMethodValidator;

public interface IIncomingMethodValidator
{
    bool Validate(MethodInfo? methodInfo, out Exception ex);
}
