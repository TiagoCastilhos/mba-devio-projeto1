using System.Net;

namespace SuperStore.Application.Exceptions;
public abstract class ServiceApplicationException : Exception
{
    public HttpStatusCode StatusCode { get; }

    protected ServiceApplicationException(string message, HttpStatusCode statusCode)
        : base(message)
    {
        StatusCode = statusCode;
    }
}