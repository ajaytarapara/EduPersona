using AutoMapper;
using EduPersona.Core.Business.IServices;
using EduPersona.Core.Data.Entities;
using EduPersona.Core.Shared.Constants;
using EduPersona.Core.Shared.Models.Request;
using EduPersona.Core.Shared.Models.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using static EduPersona.Core.Shared.Constants.Messages;
using static EduPersona.Core.Shared.ExceptionHandler.SpecificExceptions;

namespace EduPersona.Core.Business.Services
{
    public class UserProfileService : BaseService<UserProfile>, IUserProfileService
    {
        private ICurrentUserService _currentUserService;

        private IMapper _mapper;
        public UserProfileService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper) : base(unitOfWork)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<bool> CheckIsProfileCompletedAsync()
        {
            int currentUserId = _currentUserService.GetCurrentUserId();
            UserProfile userProfile = await GetFirstOrDefaultAsync(u => u.UserId == currentUserId && u.IsProfileCompleted && !u.IsDeleted);
            if (userProfile == null)
            {
                return false;
            }
            return true;
        }

        public async Task CompleteUserProfileAsync(UserProfileRequest userProfileRequest)
        {
            int currentUserId = _currentUserService.GetCurrentUserId();

            //check profession
            await IsProfessionExist(userProfileRequest.ProfessionId);

            //check designation
            await ValidateDesignationsAsync(userProfileRequest.CurrentDesignationId, userProfileRequest.TargetDesignationId, userProfileRequest.ProfessionId);

            //check skills exist
            await IsSkillExist(userProfileRequest.SkillIds);

            //create user profile
            UserProfile profileRequest = _mapper.Map<UserProfile>(userProfileRequest);
            profileRequest.UserId = currentUserId;
            profileRequest.CreatedBy = currentUserId;
            UserProfile userProfile = await AddAsync(profileRequest);
            await _unitOfWork.SaveAsync();

            //create user designation
            UserDesignation designationRequest = _mapper.Map<UserDesignation>(userProfileRequest);
            designationRequest.UserProfileId = userProfile.Id;
            designationRequest.CreatedBy = currentUserId;
            await _unitOfWork.UserDesignationRepository.AddAsync(designationRequest);

            //create user skill
            IEnumerable<UserSkill> skillRequest = userProfileRequest.SkillIds.Select(skill => new UserSkill
            {
                UserProfileId = userProfile.Id,
                CreatedBy = currentUserId,
                SkillId = skill
            });
            await _unitOfWork.UserSkillRepository.AddRangeAsync(skillRequest);

            userProfile.IsProfileCompleted = true;
            await UpdateAsync(userProfile);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateUserProfileAsync(UpdateUserProfileRequest userProfileRequest)
        {
            int currentUserId = _currentUserService.GetCurrentUserId();

            // 1. Load existing profile
            UserProfile userProfile = await GetFirstOrDefaultAsync(u => u.UserId == currentUserId && u.IsProfileCompleted && !u.IsDeleted);

            if (userProfile == null)
                throw new NotFoundException(ErrorMessage.NotExistMessage("User profile"));

            // 2. Validate master data
            await IsProfessionExist(userProfileRequest.ProfessionId);
            await ValidateDesignationsAsync(
                userProfileRequest.CurrentDesignationId,
                userProfileRequest.TargetDesignationId,
                userProfileRequest.ProfessionId);

            await IsSkillExist(userProfileRequest.SkillIds);

            // 3. Update UserProfile (map only allowed fields)
            _mapper.Map(userProfileRequest, userProfile);
            userProfile.UpdatedBy = currentUserId;
            userProfile.UpdatedAt = DateTime.UtcNow;

            await UpdateAsync(userProfile);

            // 4. Update UserDesignation
            if (userProfileRequest.TargetDesignationId != null || userProfileRequest.CurrentDesignationId != null)
                await UpdateUserDesignation(userProfileRequest, userProfile.Id, currentUserId);

            // 5. Update UserSkills (REPLACE strategy)
            await UpdateUserSkills(userProfileRequest, userProfile.Id, currentUserId);

            // 6. Save everything
            await _unitOfWork.SaveAsync();
        }

        private async Task IsSkillExist(List<int> skillIds)
        {
            if (skillIds.Distinct().Count() != skillIds.Count)
            {
                throw new BadRequestException(ErrorMessage.DuplicateSkillNotAllowed);
            }

            if (skillIds == null || !skillIds.Any())
                throw new BadRequestException(ErrorMessage.AtLeastOneSkillMessage);

            IEnumerable<Skill> existingSkills = await _unitOfWork.SkillRepository.FindAsync(s => skillIds.Contains(s.Id));

            var existingSkillIds = existingSkills.Select(s => s.Id).ToList();

            var missingSkillIds = skillIds.Except(existingSkillIds).ToList();

            if (missingSkillIds.Any())
                throw new BadRequestException(
                   ErrorMessage.InvalidSkillIdsMessage(missingSkillIds)
                );
        }

        private async Task IsProfessionExist(int? professionId)
        {
            bool isProfessionExist = await _unitOfWork.ProfessionRepository.ExistsAsync(p => p.Id == professionId && p.IsActive && !p.IsDeleted);

            if (!isProfessionExist)
            {
                throw new BadRequestException(ErrorMessage.NotExistMessage("Profession"));
            }
        }

        private async Task ValidateDesignationsAsync(int? currentDesignationId, int? targetDesignationId, int? professionId)
        {
            // checked selected designation is not same 
            if (currentDesignationId == targetDesignationId)
            {
                throw new BadRequestException(ErrorMessage.SameCurrentAndTargetDesignation);
            }

            IEnumerable<Designation>? designations = await _unitOfWork.DesignationRepository.FindAsync(d =>
                    d.Id == currentDesignationId || d.Id == targetDesignationId
                );

            List<Designation> designationList = designations.ToList();

            // check current and target designation exist
            if (!designationList.Any(d => d.Id == currentDesignationId))
                throw new BadRequestException(ErrorMessage.NotExistMessage("Current designation"));

            if (!designationList.Any(d => d.Id == targetDesignationId))
                throw new BadRequestException(ErrorMessage.NotExistMessage("Target designation"));

            // check profession exist
            bool invalidDesignation = designationList.Any(d => d.ProfessionId != professionId);

            if (invalidDesignation)
                throw new BadRequestException(ErrorMessage.DesignationProfessionMismatch);
        }

        private async Task UpdateUserSkills(UpdateUserProfileRequest userProfileRequest, int userProfileId, int currentUserId)
        {
            IEnumerable<UserSkill> existingSkills = await _unitOfWork.UserSkillRepository.GetAsync(x => x.UserProfileId == userProfileId);

            if (existingSkills.Any())
                await _unitOfWork.UserSkillRepository.DeleteRangeAsync(existingSkills);

            IEnumerable<UserSkill> newSkills = userProfileRequest.SkillIds.Select(skillId => new UserSkill
            {
                UserProfileId = userProfileId,
                SkillId = skillId,
                CreatedBy = currentUserId
            });

            await _unitOfWork.UserSkillRepository.AddRangeAsync(newSkills);
        }

        private async Task UpdateUserDesignation(UpdateUserProfileRequest userProfileRequest, int userProfileId, int currentUserId)
        {
            UserDesignation? designation = await _unitOfWork.UserDesignationRepository
               .GetFirstOrDefaultAsync(x => x.UserProfileId == userProfileId && x.IsActive && !x.IsDeleted);

            if (designation == null)
                throw new NotFoundException(ErrorMessage.NotExistMessage("User Designation"));

            designation.CurrentDesignationId = (int)userProfileRequest.CurrentDesignationId;
            designation.TargetDesignationId = (int)userProfileRequest.TargetDesignationId;
            designation.ProfessionId = (int)userProfileRequest.ProfessionId;
            designation.UpdatedBy = currentUserId;
            designation.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.UserDesignationRepository.UpdateAsync(designation);
        }

        //profession update
        public async Task ChangeProfessionAsync(ChangeProfessionRequest request)
        {
            int currentUserId = _currentUserService.GetCurrentUserId();

            // 1. Get active user profile
            UserProfile? userProfile =
                await GetFirstOrDefaultAsync(
                    x => x.UserId == currentUserId && x.IsActive && !x.IsDeleted);

            if (userProfile == null)
                throw new NotFoundException("User profile not found");

            // 2. Validate master data
            await IsProfessionExist(request.ProfessionId);
            await ValidateDesignationsAsync(
                request.CurrentDesignationId,
                request.TargetDesignationId,
                request.ProfessionId);
            await IsSkillExist(request.SkillIds);

            // 3. Deactivate current designation
            await DeactivateActiveDesignationAsync(userProfile.Id, currentUserId);

            // 4. Create new designation version
            await CreateNewDesignationAsync(request, userProfile.Id, currentUserId);

            // 5. Deactivate current skills
            await DeactivateActiveSkillsAsync(userProfile.Id, currentUserId);

            // 6. Create new skill versions
            await CreateNewSkillsAsync(request.SkillIds, userProfile.Id, currentUserId);

            // 7. Save
            await _unitOfWork.SaveAsync();
        }

        private async Task DeactivateActiveDesignationAsync(int userProfileId, int currentUserId)
        {
            var activeDesignations =
                await _unitOfWork.UserDesignationRepository.FindAsync(
                    x => x.UserProfileId == userProfileId && x.IsActive);

            if (!activeDesignations.Any())
                return;

            foreach (var designation in activeDesignations)
            {
                designation.IsActive = false;
                designation.UpdatedBy = currentUserId;
                designation.UpdatedAt = DateTimeOffset.UtcNow;
            }

            await _unitOfWork.UserDesignationRepository.UpdateRangeAsync(activeDesignations);
        }

        private async Task CreateNewDesignationAsync(ChangeProfessionRequest request, int userProfileId, int currentUserId)
        {
            var newDesignation = new UserDesignation
            {
                UserProfileId = userProfileId,
                ProfessionId = request.ProfessionId,
                CurrentDesignationId = request.CurrentDesignationId,
                TargetDesignationId = request.TargetDesignationId,
                CreatedBy = currentUserId,
                IsActive = true
            };

            await _unitOfWork.UserDesignationRepository.AddAsync(newDesignation);
        }

        private async Task DeactivateActiveSkillsAsync(int userProfileId, int currentUserId)
        {
            var activeSkills = await _unitOfWork.UserSkillRepository.FindAsync(
                x => x.UserProfileId == userProfileId && x.IsActive);

            if (!activeSkills.Any())
                return;

            foreach (var skill in activeSkills)
            {
                skill.IsActive = false;
                skill.UpdatedBy = currentUserId;
                skill.UpdatedAt = DateTimeOffset.UtcNow;
            }

            await _unitOfWork.UserSkillRepository.UpdateRangeAsync(activeSkills);
        }

        private async Task CreateNewSkillsAsync(IEnumerable<int> skillIds, int userProfileId, int currentUserId)
        {
            var newSkills = skillIds.Select(skillId => new UserSkill
            {
                UserProfileId = userProfileId,
                SkillId = skillId,
                CreatedBy = currentUserId,
                IsActive = true
            });

            await _unitOfWork.UserSkillRepository.AddRangeAsync(newSkills);
        }
        //end profession update

        //update designation
        public async Task ChangeDesignationAsync(ChangeDesignationRequest request)
        {
            int currentUserId = _currentUserService.GetCurrentUserId();

            // 1. Get active user profile
            UserProfile? userProfile =
                await GetFirstOrDefaultAsync(
                    x => x.UserId == currentUserId && x.IsActive && !x.IsDeleted);

            if (userProfile == null)
                throw new NotFoundException(ErrorMessage.NotExistMessage("User profile"));

            // 2. Get active designation
            UserDesignation? activeDesignation =
                await _unitOfWork.UserDesignationRepository.GetFirstOrDefaultAsync(
                    x => x.UserProfileId == userProfile.Id && x.IsActive && !x.IsDeleted);

            if (activeDesignation == null)
                throw new NotFoundException(ErrorMessage.NotFoundMessage("Active designation"));

            // 3. Validate new designations (same profession)
            await ValidateDesignationsAsync(
                request.CurrentDesignationId,
                request.TargetDesignationId,
                activeDesignation.ProfessionId);

            // 4. Deactivate old designation
            activeDesignation.IsActive = false;
            activeDesignation.UpdatedBy = currentUserId;
            activeDesignation.UpdatedAt = DateTimeOffset.UtcNow;

            await _unitOfWork.UserDesignationRepository.UpdateAsync(activeDesignation);

            // 5. Create new designation version
            var newDesignation = new UserDesignation
            {
                UserProfileId = userProfile.Id,
                ProfessionId = activeDesignation.ProfessionId, // SAME profession
                CurrentDesignationId = request.CurrentDesignationId,
                TargetDesignationId = request.TargetDesignationId,
                CreatedBy = currentUserId,
                IsActive = true
            };

            await _unitOfWork.UserDesignationRepository.AddAsync(newDesignation);

            // 6. Save
            await _unitOfWork.SaveAsync();
        }
        //end update designation

        //get profile
        public async Task<UserProfileResponse> GetCurrentProfileAsync()
        {
            int currentUserId = _currentUserService.GetCurrentUserId();

            // 1. Get active profile
            UserProfile? userProfile = await GetFirstOrDefaultAsync(x => x.UserId == currentUserId && x.IsActive && !x.IsDeleted);

            if (userProfile == null)
                throw new NotFoundException(ErrorMessage.NotExistMessage("User profile"));

            // 2. Get active designation
            UserDesignation? designation = await _unitOfWork.UserDesignationRepository.GetFirstOrDefaultAsync(x => x.UserProfileId == userProfile.Id && x.IsActive && !x.IsDeleted,
                q => q.Include(x => x.Profession).Include(x => x.TargetDesignation).Include(x => x.CurrentDesignation));

            // 3. Get active skills
            var skills = await _unitOfWork.UserSkillRepository.FindAsync(x => x.UserProfileId == userProfile.Id && x.IsActive && !x.IsDeleted, query => query.Include(x => x.Skill));

            // 4. Map response
            var response = _mapper.Map<UserProfileResponse>(userProfile);

            // Manual enrich (business logic)
            response.ProfessionName = designation?.Profession?.Name ?? "";
            response.CurrentDesignationName = designation?.CurrentDesignation?.Name ?? "";
            response.TargetDesignationName = designation?.TargetDesignation?.Name ?? "";
            response.Skills = skills.Select(x => x.Skill.Name).ToList();

            return response;
        }

        public async Task<IEnumerable<ProfileVersionResponse>> GetProfileVersionsAsync()
        {
            int currentUserId = _currentUserService.GetCurrentUserId();

            // 1. Get profile
            UserProfile? userProfile = await GetFirstOrDefaultAsync(x => x.UserId == currentUserId && x.IsActive && !x.IsDeleted);

            if (userProfile == null)
                throw new NotFoundException(ErrorMessage.NotExistMessage("User profile"));

            // 2. Get all designations (versions)
            var designations =
                await _unitOfWork.UserDesignationRepository.FindAsync(
                    x => x.UserProfileId == userProfile.Id, q => q.Include(x => x.CurrentDesignation).Include(x => x.TargetDesignation).Include(x => x.Profession));

            // 3. Get all skills (grouped by version timing)
            var skills =
                await _unitOfWork.UserSkillRepository.FindAsync(
                    x => x.UserProfileId == userProfile.Id, q => q.Include(x => x.Skill));

            var versions = _mapper.Map<List<ProfileVersionResponse>>(designations);

            // Manual skill association (version logic)
            foreach (var version in versions)
            {
                version.Skills = skills
                    .Where(s =>
                        s.CreatedAt >= version.CreatedAt &&
                        s.IsActive == version.IsActive)
                    .Select(s => s.Skill.Name)
                    .ToList();
            }

            return versions.OrderByDescending(x => x.CreatedAt);
        }
        //end profile
    }
}
