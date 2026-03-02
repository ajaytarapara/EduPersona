using System.Text.Json;
using EduPersona.Assesment.Shared.Constants;
using EduPersona.Assesment.Shared.Models.ExternalApiResponse;
using EduPersona.Assesment.Shared.Models.Response;
using Microsoft.AspNetCore.Mvc;
using static EduPersona.Assesment.Shared.ExceptionHandler.SpecificExceptions;

namespace EduPersona.Assesment.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {

        [HttpGet("get-access-token/{sessionId}")]
        public async Task<IActionResult> GetAccessToken(int sessionId)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://localhost:5183/api/Login/validate-session/{sessionId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new BadRequestException(ErrorMessage.AccessTokenGenerateError);
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

            return Success(result.Data.UserInfo, SuccessMessage.LoginSuccessfully);
        }

        [HttpGet("get-access-token-by-refresh-token")]
        public async Task<IActionResult> GetAccessTokenByRefreshToken()
        {
            string? refreshToken = Request.Cookies["refresh_token"];

            if (refreshToken == null)
                return Unauthorized(ErrorMessage.RefreshTokenExpired);

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsJsonAsync($"http://localhost:5183/api/Login/access-token", refreshToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new BadRequestException(ErrorMessage.AccessTokenGenerateError);
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
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddMinutes(15),
            });

            return Success(SuccessMessage.RequestSuccessful);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("access_token");
            Response.Cookies.Delete("refresh_token");
            return Success(SuccessMessage.SuccessfullyMessage("Logout"));
        }
    }
}