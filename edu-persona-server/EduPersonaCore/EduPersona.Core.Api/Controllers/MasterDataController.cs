using EduPersona.Core.Business.IServices;
using EduPersona.Core.Shared.Constants;
using EduPersona.Core.Shared.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduPersona.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class MasterDataController : BaseApiController
    {
        private readonly IMasterDataService _service;

        public MasterDataController(IMasterDataService service)
        {
            _service = service;
        }

        #region ==================== PROFESSION ====================

        // Drop down (Admin + User)
        [HttpGet("profession-drop-down")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetProfessionDropdown()
        {
            var result = await _service.GetProfessionDropdownAsync();
            return Success(result, Messages.RequestSuccessful);
        }

        // Add (Admin only)
        [HttpPost("profession")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProfession([FromBody] ProfessionRequest request)
        {
            await _service.AddProfessionAsync(request);
            return Success(Messages.SuccessfullyMessage("Profession added"));
        }

        // Update (Admin only)
        [HttpPut("profession/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProfession(int id, [FromBody] ProfessionRequest request)
        {
            await _service.UpdateProfessionAsync(id, request);
            return Success(Messages.SuccessfullyMessage("Profession updated"));
        }

        // Soft Delete (Admin only)
        [HttpDelete("profession/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProfession(int id)
        {
            await _service.DeleteProfessionAsync(id);
            return Success(Messages.SuccessfullyMessage("Profession deleted"));
        }

        #endregion


        #region ==================== SKILL ====================

        [HttpGet("skill-drop-down")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetSkillDropdown()
        {
            var result = await _service.GetSkillDropdownAsync();
            return Success(result,Messages.RequestSuccessful);
        }

        [HttpPost("skill")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSkill([FromBody] SkillRequest request)
        {
            await _service.AddSkillAsync(request);
            return Success(Messages.SuccessfullyMessage("Skill added"));
        }

        [HttpPut("skill/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSkill(int id, [FromBody] SkillRequest request)
        {
            await _service.UpdateSkillAsync(id, request);
            return Success(Messages.SuccessfullyMessage("Skill updated"));
        }

        [HttpDelete("skill/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            await _service.DeleteSkillAsync(id);
            return Success(Messages.SuccessfullyMessage("Skill deleted"));
        }

        #endregion


        #region ==================== DESIGNATION ====================

        [HttpGet("designation-dropdown/{professionId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetDesignationByProfession(int professionId)
        {
            var result = await _service.GetDesignationByProfessionAsync(professionId);
            return Success(result, Messages.RequestSuccessful);
        }

        [HttpPost("designation")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddDesignation([FromBody] DesignationRequest request)
        {
            await _service.AddDesignationAsync(request);
            return Success(Messages.SuccessfullyMessage("Designation added"));
        }

        [HttpPut("designation/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDesignation(int id, [FromBody] DesignationRequest request)
        {
            await _service.UpdateDesignationAsync(id, request);
            return Success(Messages.SuccessfullyMessage("Designation updated"));
        }

        [HttpDelete("designation/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDesignation(int id)
        {
            await _service.DeleteDesignationAsync(id);
            return Success(Messages.SuccessfullyMessage("Designation deleted"));
        }

        #endregion
    }
}
