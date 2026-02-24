using System.Text.Json;
using EduPersona.Core.Business.IServices;
using EduPersona.Core.Shared.Constants;
using EduPersona.Core.Shared.Models.ExternalApiResponse;
using EduPersona.Core.Shared.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EduPersona.Core.Shared.ExceptionHandler.SpecificExceptions;

namespace EduPersona.Core.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {
        private readonly IUserProfileService _userService;

        public AuthController(IUserProfileService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-access-token/{sessionId}")]
        public async Task<IActionResult> GetAccessToken(int sessionId)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://localhost:5183/api/Login/validate-session/{sessionId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new BadRequestException(Messages.AccessTokenGenerateError);
            }

            var data = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            ApiResponse<AccessTokenResponse>? result = JsonSerializer.Deserialize<ApiResponse<AccessTokenResponse>>(data, options);

            Response.Cookies.Append("access_token", result.Data.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(15),
            });

            Response.Cookies.Append("refresh_token", result.Data.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7),
            });

            return Success(result.Data.UserInfo, Messages.LoginSuccessfully);
        }

        [HttpGet("get-access-token-by-refresh-token")]
        public async Task<IActionResult> GetAccessTokenByRefreshToken()
        {
            string? refreshToken = Request.Cookies["refresh_token"];

            if (refreshToken == null)
                return Unauthorized(Messages.RefreshTokenExpired);

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsJsonAsync($"http://localhost:5183/api/Login/access-token", refreshToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new BadRequestException(Messages.AccessTokenGenerateError);
            }

            var data = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            ApiResponse<string>? result = JsonSerializer.Deserialize<ApiResponse<string>>(data, options);

            Response.Cookies.Append("access_token", result.Data, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(15),
            });

            return Success(Messages.RequestSuccessful);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("access_token");
            Response.Cookies.Delete("refresh_token");
            return Success(Messages.SuccessfullyMessage("Logout"));
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Success("Hello", Messages.RequestSuccessful);
        }
    }
}