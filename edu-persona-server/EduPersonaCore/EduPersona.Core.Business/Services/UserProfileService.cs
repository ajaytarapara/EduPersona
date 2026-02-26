using AutoMapper;
using EduPersona.Core.Business.IServices;
using EduPersona.Core.Data.Entities;
using EduPersona.Core.Shared.Constants;
using EduPersona.Core.Shared.Models.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using static EduPersona.Core.Shared.Constants.Messages;
using static EduPersona.Core.Shared.ExceptionHandler.SpecificExceptions;

namespace EduPersona.Core.Business.Services
{
    public class UserProfileService : BaseService<UserProfile>, IUserProfileService
    {
        private CurrentUserService _currentUserService;

        private IMapper _mapper;
        public UserProfileService(IUnitOfWork unitOfWork, CurrentUserService currentUserService, IMapper mapper) : base(unitOfWork)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<bool> CheckIsProfileCompletedAsync()
        {
            int currentUserId = _currentUserService.GetCurrentUserId();
            UserProfile userProfile = await GetFirstOrDefaultAsync(u => u.userId == currentUserId && u.IsProfileCompleted && !u.IsDeleted);
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
            UserProfile userProfile = await AddAsync(profileRequest);


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

        private async Task IsProfessionExist(int professionId)
        {
            bool isProfessionExist = await _unitOfWork.ProfessionRepository.ExistsAsync(p => p.Id == professionId && p.IsActive && !p.IsDeleted);

            if (!isProfessionExist)
            {
                throw new BadRequestException(ErrorMessage.NotExistMessage("Profession"));
            }
        }

        private async Task ValidateDesignationsAsync(int currentDesignationId, int targetDesignationId, int professionId)
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

    }
}
