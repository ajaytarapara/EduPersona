using System.Text.Json;
using EduPersona.Core.Business.IServices;
using EduPersona.Core.Shared.Constants;
using EduPersona.Core.Shared.Models.ExternalApiResponse;
using EduPersona.Core.Shared.Models.Response;
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

        [HttpGet("get-access-token")]
        public async Task<IActionResult> GetAccessToken()
        {
            if (!Request.Cookies.TryGetValue("session_id", out var sessionId))
            {
                return Unauthorized(new
                {
                    error = Messages.AccessTokenGenerateError
                });
            }

            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://localhost:5183/api/Login/validate-session/{sessionId}");

            if (!response.IsSuccessStatusCode)
            {
                return Unauthorized(new
                {
                    error = Messages.AccessTokenGenerateError
                });
            }

            var data = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            ApiResponse<AccessTokenResponse>? result = JsonSerializer.Deserialize<ApiResponse<AccessTokenResponse>>(data, options);

            Response.Cookies.Append("profile_access_token", result.Data.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(1),
            });

            Response.Cookies.Append("refresh_token", result.Data.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(5),
            });

            return Success(result.Data.UserInfo, Messages.LoginSuccessfully);
        }

        [HttpGet("get-access-token-by-refresh-token")]
        public async Task<IActionResult> GetAccessTokenByRefreshToken()
        {
            string? refreshToken = Request.Cookies["refresh_token"];

            if (refreshToken == null)
                return Unauthorized(new
                {
                    error = Messages.RefreshTokenInvalid
                });

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsJsonAsync($"http://localhost:5183/api/Login/access-token", refreshToken);

            if (!response.IsSuccessStatusCode)
            {
                // throw new UnauthorizedException(Messages.AccessTokenGenerateError);
                return Unauthorized(new
                {
                    error = Messages.RefreshTokenInvalid
                });
            }

            var data = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            ApiResponse<AccessTokenByRefreshTokenResponse>? result = JsonSerializer.Deserialize<ApiResponse<AccessTokenByRefreshTokenResponse>>(data, options);

            Response.Cookies.Append("profile_access_token", result.Data.NewAccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddMinutes(1),
            });

            Response.Cookies.Append("refresh_token", result.Data.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = result.Data.RefreshTokenExpiredAt,
            });

            Response.Cookies.Append("session_id", result.Data.SessionId.ToString(), new CookieOptions
            {
                HttpOnly = false,
                Secure = false, // true in production
                SameSite = SameSiteMode.Lax,
                Expires = result.Data.SessionExpiredAt, // new extended time
                Path = "/"
            });

            return Success(Messages.RequestSuccessful);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("profile_access_token");
            Response.Cookies.Delete("refresh_token");
            return Success(Messages.SuccessfullyMessage("Logout"));
        }
    }
}