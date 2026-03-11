using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EduPersona.Assesment.Shared.Constants;
using EduPersona.Assesment.Shared.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduPersona.Assesment.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : BaseApiController
    {
        [Authorize]
        [HttpGet("login-user-info")]
        public async Task<IActionResult> GetLogInUserInfo()
        {
            if (!Request.Cookies.TryGetValue("session_id", out var sessionId))
            {
                return Unauthorized(new
                {
                    error = ErrorMessage.AccessTokenGenerateError
                });
            }

            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://localhost:5183/api/Profile/login-user-info/{sessionId}");

            if (!response.IsSuccessStatusCode)
            {
                return Unauthorized(new
                {
                    error = ErrorMessage.AccessTokenGenerateError
                });
            }

            var data = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            ApiResponse<UserInfo>? result = JsonSerializer.Deserialize<ApiResponse<UserInfo>>(data, options);
            return Success(result?.Data, SuccessMessage.RequestSuccessful);
        }
    }
}