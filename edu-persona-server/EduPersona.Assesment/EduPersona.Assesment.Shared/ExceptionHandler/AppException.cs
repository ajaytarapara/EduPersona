using System.Net;

namespace EduPersona.Assesment.Shared.ExceptionHandler
{
    public class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        protected AppException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}