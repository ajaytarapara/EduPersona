using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduPersona.Core.Business.IServices;
using EduPersona.Core.Shared.Constants;
using EduPersona.Core.Shared.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace EduPersona.Core.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : BaseApiController
    {
        private IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet("check-profile-completed/")]
        public async Task<IActionResult> CheckIsProfileCompleted()
        {
            bool isProfileCompleted = await _userProfileService.CheckIsProfileCompletedAsync();
            return Success(isProfileCompleted, Messages.RequestSuccessful);
        }

        [HttpPut("update-profile/{userID}")]
        public async Task<IActionResult> UpdateUseProfile(int userID, [FromBody] UserProfileRequest userProfileRequest)
        {
            await _userProfileService.UpdateUserProfileAsync(userID, userProfileRequest);
            return Success(Messages.UpdateSuccessfullyMessage("UserProfile"));
        }

    }
}