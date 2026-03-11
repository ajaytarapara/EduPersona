using System.Text.Json;
using EduPersona.Assesment.Shared.Constants;
using EduPersona.Assesment.Shared.Models.ExternalApiResponse;
using EduPersona.Assesment.Shared.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EduPersona.Assesment.Shared.ExceptionHandler.SpecificExceptions;

namespace EduPersona.Assesment.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {
        [HttpGet("get-access-token")]
        public async Task<IActionResult> GetAccessToken()
        {
            if (!Request.Cookies.TryGetValue("session_id", out var sessionId))
            {
                return Unauthorized(new
                {
                    error = ErrorMessage.AccessTokenGenerateError
                });
            }

            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://localhost:5183/api/Login/validate-session/{sessionId}");

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

            ApiResponse<AccessTokenResponse>? result = JsonSerializer.Deserialize<ApiResponse<AccessTokenResponse>>(data, options);

            Response.Cookies.Append("exam_access_token", result.Data.AccessToken, new CookieOptions
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

            return Success(result.Data.UserInfo, SuccessMessage.LoginSuccessfully);
        }

        [HttpGet("get-access-token-by-refresh-token")]
        public async Task<IActionResult> GetAccessTokenByRefreshToken()
        {
            string? refreshToken = Request.Cookies["refresh_token"];

            if (refreshToken == null)
                return Unauthorized(new
                {
                    error = ErrorMessage.RefreshTokenInvalid
                });

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsJsonAsync($"http://localhost:5183/api/Login/access-token", refreshToken);

            if (!response.IsSuccessStatusCode)
            {
                // throw new UnauthorizedException(ErrorMessage.AccessTokenGenerateError);
                return Unauthorized(new
                {
                    error = ErrorMessage.RefreshTokenInvalid
                });
            }

            var data = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            ApiResponse<AccessTokenByRefreshTokenResponse>? result = JsonSerializer.Deserialize<ApiResponse<AccessTokenByRefreshTokenResponse>>(data, options);

            Response.Cookies.Append("exam_access_token", result.Data.NewAccessToken, new CookieOptions
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

            return Success(SuccessMessage.RequestSuccessful);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("exam_access_token");
            Response.Cookies.Delete("refresh_token");
            return Success(SuccessMessage.SuccessfullyMessage("Logout"));
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Success("Hello", SuccessMessage.SuccessfullyMessage("Access grant"));
        }
    }
}