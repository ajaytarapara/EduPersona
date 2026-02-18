using IdentityProvider.Business.IServices;
using IdentityProvider.Shared.Constants;
using IdentityProvider.Shared.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProvider.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register registerRequest)
        {
            return Success(await _userService.RegisterAsync(registerRequest), ApiMessages.UserRegisteredSuccessfully);
        }

    }
}
