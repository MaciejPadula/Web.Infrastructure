using System.Net;

namespace Web.Infrastructure.Microservices.Client.Exceptions
{
    public class MicroserviceResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        internal MicroserviceResponseException(HttpResponseMessage? response)
            : this(response?.ReasonPhrase ?? string.Empty, response?.StatusCode ?? HttpStatusCode.NotFound)
        { }

        internal MicroserviceResponseException(string message, HttpStatusCode statusCode, Exception innerException = default!)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
