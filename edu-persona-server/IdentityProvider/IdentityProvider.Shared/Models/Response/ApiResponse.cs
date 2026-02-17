using System.Net;

namespace IdentityProvider.Shared.Models.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public List<string> Errors { get; set; }

        public ApiResponse(bool success, HttpStatusCode statusCode, string message, T data = default, List<string> errors = null)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
            Data = data;
            Errors = errors;
        }
    }
}