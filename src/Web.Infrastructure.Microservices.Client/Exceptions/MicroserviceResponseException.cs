using System.Net;

namespace Web.Infrastructure.Microservices.Client.Exceptions
{
    public class MicroserviceResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        internal MicroserviceResponseException(HttpResponseMessage? response)
            : this(response?.ReasonPhrase ?? string.Empty, response?.StatusCode ?? HttpStatusCode.NotFound)
        { }

        internal MicroserviceResponseException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
