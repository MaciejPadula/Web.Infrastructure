namespace Web.Infrastructure.Microservices.Client.Models;

public class MicroserviceClientConfiguration
{
    public bool RetryOnError { get; set; } = false;
    public int NumberOfRetries { get; set; } = 0;
    public TimeSpan Timeout { get; set; } = TimeSpan.Zero;
    public TimeSpan RetryInterval { get; set; } = TimeSpan.Zero;
    internal Uri BaseUrl { get; set; } = default!;

    internal MicroserviceClientConfiguration() { }
}
