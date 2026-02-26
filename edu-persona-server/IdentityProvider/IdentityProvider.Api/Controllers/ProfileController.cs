using IdentityProvider.Business.IServices;
using IdentityProvider.Shared.Constants;
using IdentityProvider.Shared.Models.Request;
using IdentityProvider.Shared.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static IdentityProvider.Shared.Constants.Enums;

namespace IdentityProvider.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles = nameof(Roles.User))]
    public class ProfileController : BaseApiController
    {
        private readonly IProfileService _profileService;

        private readonly ICurrentUserService _currentUserService;
        public ProfileController(IProfileService profileService, ICurrentUserService currentUserService)
        {
            _profileService = profileService;
            _currentUserService = currentUserService;
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            int userId = _currentUserService.GetCurrentUserId();

            await _profileService.UpdateProfileAsync(userId, request);

            return Success(ApiMessages.SuccessfullyMessage("Profile Updated"));
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            int userId = _currentUserService.GetCurrentUserId();

            BasicProfileResponse basicProfile =await  _profileService.GetUserBasicProfile(userId);

            return Success(basicProfile,ApiMessages.RequestSuccessful);
        }

    }
}
