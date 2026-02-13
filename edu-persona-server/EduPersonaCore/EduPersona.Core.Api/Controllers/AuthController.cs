using EduPersona.Core.Business.IServices;
using EduPersona.Core.Data.Entities;
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

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            string user = "test";
            if (user == "test")
            {
                throw new NotFoundException("Not found");
            }
            return Success(user, "User fetched successfully");
        }
      
    }
}