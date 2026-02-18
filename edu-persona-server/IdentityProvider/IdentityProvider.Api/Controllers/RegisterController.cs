using IdentityProvider.Business.IServices;
using IdentityProvider.Shared.Constants;
using IdentityProvider.Shared.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProvider.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : BaseApiController
    {
        private readonly IRegisterService _userService;
        public RegisterController(IRegisterService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            await _userService.RegisterAsync(registerRequest);
            return Success(ApiMessages.UserRegisteredSuccessfully);
        }
    }
}
