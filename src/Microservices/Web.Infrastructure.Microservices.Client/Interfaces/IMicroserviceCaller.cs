namespace Web.Infrastructure.Microservices.Client.Interfaces
{
    internal interface IMicroserviceCaller
    {
        Task<object?> Call(string methodName, string serviceName, object?[]? args, Type type);
        Task<TResult?> Call<TResult>(string methodName, string serviceName, object?[]? args);
    }
}
