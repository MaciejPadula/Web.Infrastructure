﻿namespace Web.Infrastructure.Microservices.Client.Logic.HttpMessageProvider
{
    public interface IHttpMessageProvider
    {
        HttpRequestMessage Provide(Uri baseUrl, string endpoint, object?[]? args, HttpMethod method);
    }
}