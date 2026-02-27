using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduPersona.Core.Business.IServices;
using EduPersona.Core.Shared.Constants;
using EduPersona.Core.Shared.Models.Request;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost("complete-profile")]
        public async Task<IActionResult> CompleteUseProfile([FromBody] UserProfileRequest userProfileRequest)
        {
            await _userProfileService.CompleteUserProfileAsync(userProfileRequest);
            return Success(Messages.ProfileCompletedSuccessful);
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileRequest request)
        {
            await _userProfileService.UpdateUserProfileAsync(request);
            return Success(Messages.UpdateSuccessfullyMessage("Profile"));
        }

        [HttpPost("change-profession")]
        public async Task<IActionResult> ChangeProfession([FromBody] ChangeProfessionRequest request)
        {
            await _userProfileService.ChangeProfessionAsync(request);
            return Success(Messages.UpdateSuccessfullyMessage("Profession"));
        }

        [HttpPost("change-designation")]
        public async Task<IActionResult> ChangeDesignation([FromBody] ChangeDesignationRequest request)
        {
            await _userProfileService.ChangeDesignationAsync(request);
            return Success(Messages.UpdateSuccessfullyMessage("Designation"));
        }
    }
}