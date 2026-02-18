using IdentityProvider.Shared.ExceptionHandler;
using IdentityProvider.Shared.Models.Response;
using System.Net;
using System.Text.Json;


namespace IdentityProvider.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
                string message = "An unexpected error occurred";

                if (ex is AppException exception)
                {
                    statusCode = exception.StatusCode;
                    message = exception.Message;
                }

                httpContext.Response.ContentType = "application/json"; // tells the client response in JSON
                httpContext.Response.StatusCode = (int)statusCode; // set actual status code 

                var response = new ApiResponse<object>(
                    false,
                    statusCode,
                    message,
                    null,
                    null
                );

                //convert C# model to JSON 
                await httpContext.Response.WriteAsync(
                    JsonSerializer.Serialize(response)
                );
            }
        }
    }
}