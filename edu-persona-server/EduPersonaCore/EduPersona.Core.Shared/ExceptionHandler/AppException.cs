using System.Net;

namespace EduPersona.Core.Shared.ExceptionHandler
{
    public abstract class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        protected AppException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}