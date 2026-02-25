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
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddHours(8),
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
            string newTokens = await _loginService.GetAccessTokenAsync(refreshToken);
            return Success(newTokens, ApiMessages.RequestSuccessful);
        }

        [HttpGet("logout/{sessionId}")]
        public async Task<IActionResult> Logout(int sessionId)
        {
            HttpClient httpClient = new HttpClient();
            var profileAppResponse = await httpClient.GetAsync($"http://localhost:5238/api/Auth/logout");
            if (!profileAppResponse.IsSuccessStatusCode)
            {
                throw new BadRequestException(ApiMessages.LogoutFail);
            }
            await _loginService.LogoutAsync(sessionId);
            Response.Cookies.Delete("session_id");
            return Success(ApiMessages.SuccessfullyMessage("Logout"));
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            IdpLoginResponse? response = await _loginService.GoogleLoginAsync(request.Code);

            return Success(response, ApiMessages.RequestSuccessful);
        }
    }
}