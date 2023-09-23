using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using System.Net;
using Web.Infrastructure.Microservices.Client.Interfaces;
using Web.Infrastructure.Microservices.Client.Models;

namespace Web.Infrastructure.Microservices.Client.Logic;

internal class HttpClientWrapper : IHttpClientWrapper
{
    private readonly HttpClient _httpClient;
    private readonly IAsyncPolicy<HttpResponseMessage>? _policy;
    private readonly ILogger<HttpClientWrapper> _logger;

    public HttpClientWrapper(HttpClient httpClient, MicroserviceClientConfiguration configuration, ILogger<HttpClientWrapper> logger)
    {
        _httpClient = httpClient;
        _httpClient.Timeout = configuration.Timeout;
        _logger = logger;

        if (!configuration.RetryOnError) return;

        Func<int, TimeSpan> retrying = retryAttempt =>
        {
            _logger.LogInformation("Retrying: {ret}", retryAttempt);
            return configuration.RetryInterval;
        };

        _policy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(response => response.StatusCode != HttpStatusCode.OK)
            .Or<Exception>()
            .WaitAndRetryAsync(configuration.NumberOfRetries, retrying);
    }

    public async Task<HttpResponseMessage> SendAsync(Func<HttpRequestMessage> requestGenerator)
    {
        if (_policy is null)
        {
            return await _httpClient.SendAsync(requestGenerator());
        }

        return await _policy.ExecuteAsync(() => _httpClient.SendAsync(requestGenerator()));
    }

    public void Dispose() =>
        _httpClient.Dispose();
}
