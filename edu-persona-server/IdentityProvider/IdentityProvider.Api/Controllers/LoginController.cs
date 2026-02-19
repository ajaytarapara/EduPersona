using IdentityProvider.Business.IServices;
using IdentityProvider.Shared.Constants;
using IdentityProvider.Shared.Models.Request;
using IdentityProvider.Shared.Models.Response;
using Microsoft.AspNetCore.Mvc;

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
            LoginResponse loginResponse = await _loginService.LoginAsync(loginRequest);
            Response.Cookies.Append("access_token", loginResponse.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddMinutes(60),
            });

            Response.Cookies.Append("refresh_token", loginResponse.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(7),
            });

            var response = new
            {
                userInfo = loginResponse.UserInfo,
                sessionId = loginResponse.SessionId
            };

            return Success(response, ApiMessages.LoginSuccessfully);
        }
    }
}