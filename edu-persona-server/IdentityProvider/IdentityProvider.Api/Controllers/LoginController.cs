using IdentityProvider.Business.IServices;
using IdentityProvider.Shared.Constants;
using IdentityProvider.Shared.Models.Request;
using IdentityProvider.Shared.Models.Response;
using Microsoft.AspNetCore.Mvc;
using static IdentityProvider.Shared.ExceptionHandler.SpecificExceptions;

namespace IdentityProvider.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : BaseApiController
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            IdpLoginResponse response = await _loginService.LoginAsync(loginRequest);
            Response.Cookies.Append("session_id", response.SessionId.ToString(), new CookieOptions
            {
                HttpOnly = false,
                Secure = false, // true in prod
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddMinutes(5),
                Path = "/"
            });
            return Success(response, ApiMessages.RequestSuccessful);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("validate-session/{sessionId}")]
        public async Task<IActionResult> ValidateSession(int sessionId)
        {
            LoginResponse loginResponse = await _loginService.ValidateSessionAsync(sessionId);
            return Success(loginResponse, ApiMessages.RequestSuccessful);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("access-token")]
        public async Task<IActionResult> GetAccessToken([FromBody] string refreshToken)
        {
            AccessTokenByRefreshTokenResponse response = await _loginService.GetAccessTokenAsync(refreshToken);
            return Success(response, ApiMessages.RequestSuccessful);
        }

        [HttpGet("logout/{userId}")]
        public async Task<IActionResult> Logout(int userId)
        {
            // if (!Request.Cookies.TryGetValue("session_id", out var sessionId))
            // {
            //     return Unauthorized(new
            //     {
            //         error = ApiMessages.SessionExpired
            //     });
            // }

            HttpClient httpClient = new HttpClient();
            var profileAppResponse = await httpClient.GetAsync($"http://localhost:5238/api/Auth/logout");
            if (!profileAppResponse.IsSuccessStatusCode)
            {
                throw new BadRequestException(ApiMessages.LogoutFail);
            }
            await _loginService.LogoutAsync(userId);
            Response.Cookies.Delete("session_id");
            Response.Cookies.Delete("refresh_token");
            return Success(ApiMessages.RequestSuccessful);
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            IdpLoginResponse? response = await _loginService.GoogleLoginAsync(request.Code);

            Response.Cookies.Append("session_id", response.SessionId.ToString(), new CookieOptions
            {
                HttpOnly = false,
                Secure = false, // true in prod
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddMinutes(5),
            });

            return Success(response, ApiMessages.RequestSuccessful);
        }

    }
}