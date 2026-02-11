using System.Net;
using EduPersona.Core.Shared.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace EduPersona.Core.Api.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected IActionResult Success<T>(T data, string message)
        {
            return Ok(new ApiResponse<T>(
                true,
                HttpStatusCode.OK,
                message,
                data
            ));
        }

        protected IActionResult Error(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest, List<string> errors = null)
        {
            return StatusCode((int)statusCode, new ApiResponse<object>(
            false,
            statusCode,
            message,
            null,
            errors
        )
    );
        }
    }
}