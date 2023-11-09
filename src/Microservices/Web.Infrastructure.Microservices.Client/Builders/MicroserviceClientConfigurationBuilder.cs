using Web.Infrastructure.Microservices.Client.Models;

namespace Web.Infrastructure.Microservices.Client.Builders;

public class MicroserviceClientConfigurationBuilder
{
    private readonly MicroserviceClientConfiguration _configuration;

    public MicroserviceClientConfigurationBuilder(MicroserviceClientConfiguration configuration)
    {
        _configuration = configuration;
    }

    public MicroserviceClientConfigurationBuilder()
    {
        _configuration = new MicroserviceClientConfiguration();
    }

    public MicroserviceClientConfigurationBuilder WithRetries(int numberOfRetries, TimeSpan retryInterval)
    {
        _configuration.RetryOnError = true;
        _configuration.NumberOfRetries = numberOfRetries;
        _configuration.RetryInterval = retryInterval;
        return this;
    }

    public MicroserviceClientConfigurationBuilder WithRequestTimeout(TimeSpan timeout)
    {
        _configuration.Timeout = timeout;
        return this;
    }

    public MicroserviceClientConfiguration Build() => _configuration;

    public static MicroserviceClientConfigurationBuilder GetDeafultConfig() =>
        new MicroserviceClientConfigurationBuilder()
            .WithRequestTimeout(TimeSpan.FromSeconds(5));

    public static MicroserviceClientConfigurationBuilder GetLongRunningConfig() =>
        new MicroserviceClientConfigurationBuilder()
            .WithRequestTimeout(TimeSpan.FromMinutes(5));
}
